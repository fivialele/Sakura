// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Luis;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace LuisSample
{
    public class MyLuisSample : ActivityHandler
    {
        private Microsoft.BotBuilderSamples.LuisHelper.LuisHelper _luisHelper;
        public MyLuisSample(Microsoft.BotBuilderSamples.LuisHelper.LuisHelper luisHelper)
        {
            _luisHelper = luisHelper;
        }
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($":)"), cancellationToken);
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
                    await turnContext.SendActivityAsync(MessageFactory.Text($"None"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Amused:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Amused"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Angry:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Angry"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Anxious:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Anxious"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Bored:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Bored"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Confident:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Confident"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Disappointed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Disappointed"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Embarrassed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Embarrassed"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Flirty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Flirty"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Frightened:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Frightened"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Guilty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Guilty"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Happy:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Happy"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Hungry:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hungry"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Insecure:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Insecure"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Jealous:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Jealous"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Sad:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sad"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Safe:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Safe"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Sick:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Sick"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Stressed:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Stressed"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Thirsty:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Thirsty"), cancellationToken);
                    break;
                case LuisForSakuraFirstTry.Intent.Feel_Tired:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Tired"), cancellationToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
