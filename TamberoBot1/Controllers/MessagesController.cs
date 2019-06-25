using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace TamberoBot1
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                
                await Conversation.SendAsync(activity, () => new TamberoDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }





        [LuisModel("", "")]
        [Serializable]
        public class TamberoDialog : LuisDialog<object>
        {
            [LuisIntent("")]
            public async Task None(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("What can I do for you today?");
                context.Wait(MessageReceived);
            }

            [LuisIntent("None")]
            public async Task Default(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("Sorry, I didn't quite get that.");
                context.Wait(MessageReceived);
            }

            [LuisIntent("Hello")]
            public async Task Hello(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("Hello farmer, can I help you?");
                context.Wait(MessageReceived);
            }


            [LuisIntent("AnimalStatus")]
            public async Task AnimalStatus(IDialogContext context, LuisResult result)
            {
                await context.PostAsync("Acá va el estado general del animal");
                context.Wait(MessageReceived);
            }

            [LuisIntent("AnimalLastProduction")]
            public async Task AnimalLastMilk(IDialogContext context, LuisResult result)
            {
                string msg;
                Animal.RootObject currentAnimal = await ConnectionTamberoAsyncAnimal("AnimalLastProduction");
                msg = "The last production of animal " + currentAnimal.animal.rp_number + " is " + currentAnimal.animal.last_production;
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }

            [LuisIntent("AnimalCurrentFeed")]
            public async Task CurrentFeed(IDialogContext context, LuisResult result)
            {
                string msg;
                Animal.RootObject currentAnimal = await ConnectionTamberoAsyncAnimal("AnimalCurrentFeed");
                msg = "The current feed of animal " + currentAnimal.animal.rp_number + " is " + currentAnimal.animal.current_feed;
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }

            [LuisIntent("AnimalCurrentHerd")]
            public async Task CurrentHerd(IDialogContext context, LuisResult result)
            {
                string msg;
                Animal.RootObject currentAnimal = await ConnectionTamberoAsyncAnimal("AnimalCurrentHerd");
                msg = "The herd of animal " + currentAnimal.animal.rp_number + " is " + currentAnimal.animal.current_herd + ".";
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }

            [LuisIntent("HeatStatus")]
            public async Task HeatStatus(IDialogContext context, LuisResult result)
            {
                string msg;
                Animal.RootObject currentAnimal = await ConnectionTamberoAsyncAnimal("HeatStatus");
                msg = "The heat status of aninmal " + currentAnimal.animal.rp_number + " is " + currentAnimal.animal.heat_status + "%.";
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }


            [LuisIntent("AnimalControlTotal")]
            public async Task AnimalControlTotal(IDialogContext context, LuisResult result)
            {
                string msg;
                Farm.RootObject currentFarm = await ConnectionTamberoAsyncFarm("AnimalControlTotal");
                msg = "The last total produccion of your farm is " + currentFarm.farm.animalControlTotal;
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }
        }



        public static async Task<Farm.RootObject> ConnectionTamberoAsyncFarm(string method)
        {
            Farm.RootObject currentFarm = new Farm.RootObject();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://beta.tambero.com");
                string url = string.Format("/apiv2/bot?method=" + method + "&apilicense=&apilang=es_AR&userid=&apikey=");
                var response = await client.GetAsync(url);
                if (HttpStatusCode.OK.ToString() == response.StatusCode.ToString())
                {
                    var result_query = response.Content.ReadAsStringAsync().Result;
                    currentFarm = JsonConvert.DeserializeObject<Farm.RootObject>(result_query);
                }
                return currentFarm;
            }
            catch (Exception ex)
            {
                return currentFarm;
            }
        }

        public static async Task<Animal.RootObject> ConnectionTamberoAsyncAnimal(string method)
        {
            Animal.RootObject currentAnimal = new Animal.RootObject();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://beta.tambero.com");
                string url = string.Format("/apiv2/bot?method=" + method + "&apilicense=&apilang=es_AR&userid=&apikey=&rp_number=");
                var response = await client.GetAsync(url);
                if (HttpStatusCode.OK.ToString() == response.StatusCode.ToString())
                {
                    var result_query = response.Content.ReadAsStringAsync().Result;
                    currentAnimal = JsonConvert.DeserializeObject<Animal.RootObject>(result_query);
                }
                return currentAnimal;
            }
            catch (Exception ex)
            {
                return currentAnimal;
            }
        }
    }
}