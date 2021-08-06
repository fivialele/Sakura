// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Luis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.BotBuilderSamples;
using Microsoft.BotBuilderSamples.Bots;
using Microsoft.BotBuilderSamples.Dialog;
using Microsoft.Extensions.Configuration;
using SP = Microsoft.SharePoint.Client;

namespace LuisSample
{
    public class MyLuisSample : ActivityHandler
    {   //aka luis dialog
        private Microsoft.BotBuilderSamples.LuisHelper.LuisHelper _luisHelper;
        private readonly IConfiguration _configuration;//need it if you make the connection to sharepoint
        private readonly IBotServices _services;

        protected readonly ConversationState ConversationState;
        protected readonly UserState UserState;
        public MyLuisSample(Microsoft.BotBuilderSamples.LuisHelper.LuisHelper luisHelper, IConfiguration configuration, IBotServices services, ConversationState conversationState, UserState userState)
        {
            _luisHelper = luisHelper;
            _configuration = configuration;
            _services = services;

            ConversationState = conversationState;
            UserState = userState;
        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello and welcome! I am Sakura, your friend and personal coach :)"), cancellationToken);
                }
            }
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var result = await _luisHelper.RecognizeAsync<LuisForSakuraFirstTry>(turnContext, cancellationToken);
            var topIntent = result.TopIntent().intent;
            

            switch (topIntent)
            {
                case LuisForSakuraFirstTry.Intent.None:
                    {
                        RootDialog rootDialog = new RootDialog(_services, _configuration);
                        QnABot<RootDialog> qnABot = new QnABot<RootDialog>(ConversationState, UserState, rootDialog);
                        
                        await turnContext.SendActivityAsync(MessageFactory.Text("__"), cancellationToken);
                        break;
                    }
                case LuisForSakuraFirstTry.Intent.Feel_Amused:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"I am happy to see you're amused. Can you tell me what made you that way?"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Angry:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"When angry, first keep your mind cool and take a deep breath. \n Think of logic about how you are going to answer, don't use the angry tone but a concern, serious tone. \n If you are being humiliated by someone, then count numbers or look at your shoe laces or distract your mind. \n Hope this helps you out. When i am angry, i usually eat some ice cream"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Anxious:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"I’m always here for you. Try to go to a more silent place or go for a walk . Can i do something to help you?"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Bored:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"If you're feeling bored in any way, my advice is to visit this site, which gives you interesting activities ideas : https://www.thecut.com/article/things-to-do-when-bored.html"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Confident:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"I am happy to see that you're feeling confident today. Indeed, self confidence is a super power add once you start believing in yourself, magic starts to happen"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Disappointed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"It is normal that people or events in our lives are not always meeting our expectations. But keep in mind that brighter days are going to come into our lives. For the moment, it is recommended that you:  acknowledge your unmet needs, take care of yourself,  decide if you need to speak up and most importantly  examine your expectations and discuss them with your close circle of friends or family."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Embarrassed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Understand That Embarrassments Are in the Past \n When learning to cope with embarrassment, the first big thing that will help you is to realize that embarrassing moments are in the past. These things have already occurred and there is nothing that you can do to change them.It might have been tough to get through those moments, but you’re here now and everything is okay.Something that happened in the past can’t hurt you and it doesn’t have to define who you are.It’s going to be best for you to let go of the past so that you can move on and enjoy the future.This will be easier said than done for some people, but you can stop living in the past and focusing on embarrassing moments if you put the effort in.\n Know That You Don’t Need to Apologize for Feeling Embarrassed\n These embarrassing things that happened to you might make you feel like you’re ashamed.Some people feel a deep sense of shame when they’re embarrassed and this can make things worse.You might even feel the need to apologize because you’re feeling embarrassed, but this isn’t actually necessary.There’s no reason to apologize for feeling embarrassed and this is usually a natural response to certain things.For example, most people are going to be a bit embarrassed if someone walks in on them taking a shower or if they trip in front of a group of people. These things just happen and you didn’t do anything wrong by having an embarrassing reaction to these events.\n \n Talk to Someone About How You’re Feeling\n Coping with embarrassment is really about figuring out how to get back to normal. You want to feel normal and at ease, so it makes sense to talk to people that make you feel safe. If you’re struggling with feelings of embarrassment, then you should try to talk to people that you trust and respect very much.Let them know how you’re feeling and tell them what happened if you’re comfortable doing that. You’ll likely find that these people will put things into perspective and you’ll feel much better. It’ll be easier for you to see that people aren’t judging you for having gone through this embarrassing moment and that the embarrassment is just living in your head.Having a strong support system in place should make coping with embarrassment much simpler than it would otherwise be."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Flirty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Flirty"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Frightened:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Allow yourself to sit with your fear for 2-3 minutes at a time. Breathe with it and say, “It’s okay. It feels lousy but emotions are like the ocean—the waves ebb and flow.” Have something nurturing planned immediately after your 2-3 minute sitting period is completed: Call the good friend waiting to hear from you; immerse yourself in an activity you know is enjoyable and engrossing. \n Write down the things you are grateful for. Look at the list when you feel you’re in a bad place.Add to the list.\n Remind yourself that your anxiety is a storehouse of wisdom.Write a letter, “Dear Anxiety, I am no longer intimidated by you.What can you teach me ?”\n Exercise.Exercise can refocus you(your mind can only focus on one thing at a time).Whether you go on a short walk, head to a boxing gym for an all-out sweat session, or turn on a 15 - minute yoga video at home, exercise is good for you and it will ground you and help you feel more capable.\n Use humor to deflate your worst fears.For instance, what are some ridiculous worst -case scenarios that might happen if you accept an invitation to deliver a speech to a crowd of 500 people? I might pee in my pants at the podium*** I will be arrested for giving the worst speech in history * **My first boyfriend(girlfriend) will be in the audience and heckle me.\n Appreciate your courage.Doreen would tell herself during difficult times, “Every time I don’t allow fear to keep me from doing something that scares me, I am making myself stronger and less likely to let the next fear attack stop me.”"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Guilty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"It is normal to experience feeling guilty in your life. There are some times in which you might not reach your own expectations or some situations made you feel not so good about yourself. yet, it is important that you try to get out of this state. Some advice for reaching this might be: recognize your fault and apologize, accept the situation you are in, but also learn from it. Ask yourself: What led to the mistake? Explore triggers that prompted your action and any feelings that tipped you over the edge. \n What would you do differently now?\n What did your actions tell you about yourself? Do they point to any specific behaviors you can work on?Instead of feeling guilty when you struggle, cultivate gratitude by: thanking loved ones for their kindness,\n making your appreciation clear,\n acknowledging any opportunities you’ve gained as a result of their support,\n committing to paying this support forward once you’re on more solid ground"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Happy:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Resolve to keep happy, and your joy and you shall form an invincible host against difficulties."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Hungry:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"A nourishing meal might help you with how you feel. Would you like me to suggest one of the delivery apps available online?"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Insecure:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Feeling insecure is hard enough, and beating yourself up for having those emotions in the first place won't do you any good. So as a starting point, I recommend dropping the self-judgment. Accept the fact that you feel insecure about something, and focus instead on doing the work to shift it. A little self-love can really go a long way. Overcoming insecurity is a journey, and it doesn’t happen overnight. So take that pressure off yourself. You can start making progress by simply taking little steps to build up your confidence and push yourself out of your comfort zone in ways where the stakes aren’t so high. If social insecurities are your struggle, begin by just saying hello to someone new or talking to just one person at a party. It’s those small steps that eventually give you the confidence to do the bigger, scarier things."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Jealous:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Examining your jealous feelings can give you insight on where they come from. Whether your jealousy stems from insecurity, fear, or past relationship patterns, knowing more about the causes can help you figure out how to confront it. \n Maybe you have an open conversation with your supervisor about getting on track for promotion, resolve to try a different approach to dating, or talk to your partner about your feelings. Try these strategies to distract yourself from jealous thoughts before they become overwhelming:\n\n    Write down what you feel. \n Take a walk. \n Give yourself space by leaving the situation.\n Take 10 minutes to do something calming."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Sad:
                    await turnContext.SendActivityAsync(MessageFactory.Text($" I’m really sorry you’re going through this. I’m here for you if you need me."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Safe:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"You're safety and happiness is my puprose. I am glad i can see you in the right environment"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Sick:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Take some rest. Make yourself a tea and take a day off. If you experience feeling worse, notify me to call your family doctor."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Stressed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sometimes it is OK to take a break. Even the most hardworking people need some rest. A walk in nature, or practicing your hobbies, having a drink with your friends might help as well with this. We need to live our lives at our fullest so maybe it is time to realise that work is not everything we have. We also have friends and family who love us and are supportive and ready to listen to our problems if we ask them to."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Thirsty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Keeping yourself hydrated might help your skin look younger and your body works at its best."), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Tired:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Get some rest. Exhaustion is not an option."), cancellationToken);
                    break;
                default:
                    //string specificMessage = FetchMessage(Convert.ToString(topIntent));
                    //await turnContext.SendActivityAsync(MessageFactory.Text($"{specificMessage}"), cancellationToken);
                    //break;
                    throw new ArgumentOutOfRangeException();
            }

        }

        /*private string FetchMessage(string topIntent)
        {
            const string siteUrl = "https://ubbcluj.sharepoint.com/sites/KB";
            const string listName = "Emotions";
            string response = string.Empty;

            string uName = Convert.ToString(_configuration["SPSiteLoginId"]); 
            string password = Convert.ToString(_configuration["SPSitePwd"]);
            var securePassword = new SecureString();

            // Starting with ClientContext, the constructor requires a URL to the
            // server running SharePoint.
            try
            {
                foreach (var c in password)
                {
                    //Random random = new Random();
                    //char randomChar = Convert.ToChar(random.Next(33,126)); //random character: Big/small letter, sign or number
                    securePassword.AppendChar(c);
                }
                //SP.ClientContext context = new SP.ClientContext(siteUrl);

                var authManager = new OfficeDevPnP.Core.AuthenticationManager();
                // This method calls a pop up window with the login page and it also prompts  
                // for the multi factor authentication code.  
                SP.ClientContext context = authManager.GetWebLoginClientContext(siteUrl);
                // The obtained ClientContext object can be used to connect to the SharePoint site.  
                SP.Web web = context.Web;

                //var credentials = new SP.SharePointOnlineCredentials(uName, securePassword);
                //context.Credentials = credentials;

                SP.List emotionList = web.Lists.GetByTitle(listName);

                SP.CamlQuery camlQuery = new SP.CamlQuery();
                camlQuery.ViewXml = $"<View><Query><Where>" +
                        $"<Eq><FieldRef Name = 'Title/'><Value Type='Text'>{topIntent}</Value><Eq>" +
                        $"<?Where></Query><RowLimit>1</RowLimit></View>";
                SP.ListItemCollection items = emotionList.GetItems(camlQuery);

                context.Load(items);

                context.ExecuteQuery();

                foreach (SP.ListItem listItem in items)
                {
                    // We have all the list item data. For example, Title.
                    response = Convert.ToString(listItem["Message"]);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }*/
    }
}
