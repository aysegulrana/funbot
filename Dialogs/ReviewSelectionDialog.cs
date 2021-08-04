using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;

namespace FunBot.Dialogs
{
    public class ReviewSelectionDialog : ComponentDialog
    {
        // Define a "done" response for the company selection prompt.
        private const string DoneOption = "Bitir";

        // Define value names for values tracked inside the dialogs.
        private const string CompaniesSelected = "value-companiesSelected";

        // Define the company choices for the company selection prompt.
        private readonly string[] _companyOptions = new string[]
        {
            "Alışveriş yapmak istiyorum.🌍",
            "Günümü güzelleştirecek bir film izlemek istiyorum.🎬",
            "Beni alıp götürecek bir dizi önerisi istiyorum. 🎥",
            "Yeni müzikler keşfetmek istiyorum. 🎧",
            "Kitap okumak istiyorum. 📚"
        };

        public ReviewSelectionDialog()
            : base(nameof(ReviewSelectionDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));

            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
                {
                    SelectionStepAsync,
                    LoopStepAsync,
                }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> SelectionStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            // Continue using the same selection list, if any, from the previous iteration of this dialog.
            var list = stepContext.Options as List<string> ?? new List<string>();
            stepContext.Values[CompaniesSelected] = list;

            // Create a prompt message.
            string message;
            if (list.Count is 0)
            {
                message = $"Bugün nasıl vakit geçirmek istersin? `{DoneOption}` seçimi ile akıştan çıkabilirsin.";
            }
            else
            {
                if (list[0].Contains("yapmak"))
                {
                    Attachment Haber()
                    {
                        var heroCard = new ThumbnailCard
                        {
                            Title = "FunBot",
                            Subtitle = "Alışveriş Siteleri",
                            Text = "Alışveriş yapabileceğin e-ticaret sitelerine aşağıdan ulaşabilirsin:",
                            Images = new List<CardImage> { new CardImage(" ") },
                            Buttons = new List<CardAction> {
                                new CardAction(ActionTypes.Call, "amazon", value: "https://www.amazon.com.tr/"),
                                new CardAction(ActionTypes.OpenUrl, "n11", value: "https://www.n11.com/"),
                                new CardAction(ActionTypes.OpenUrl, "trendyol", value: "https://www.trendyol.com/"),
                                new CardAction(ActionTypes.OpenUrl, "hepsiburada", value: "https://www.hepsiburada.com/"),
                                new CardAction(ActionTypes.OpenUrl, "gittigidiyor", value: "https://www.gittigidiyor.com/"),
                                new CardAction(ActionTypes.OpenUrl, "Çiçek Sepeti", value: "https://www.ciceksepeti.com/"),
                            }
                        };
                        return heroCard.ToAttachment();
                    }
                    Activity reply = MessageFactory.Text("Haber kanallarını inceleyebilirsiniz:");
                    reply.AttachmentLayout = AttachmentLayoutTypes.List;

                    reply.Attachments.Add(Haber());

                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);



                }

                if (list[0].Contains("film")) //else if??
                {
                    Attachment Film()
                    {
                        var heroCard = new ThumbnailCard
                        {
                            Title = "Fun Bot",
                            Subtitle = "Film Platformları",
                            Text = "Sitelere aşağıdaki linklerden ulaşabilirsiniz.",
                            Images = new List<CardImage> { new CardImage("https://upload.wikimedia.org/wikipedia/tr/0/07/Do%C4%9Fu%C5%9F_Yay%C4%B1n_Grubu_logosu.png") },
                            Buttons = new List<CardAction> {
                                new CardAction(ActionTypes.Call, "Netflix", value: "https://www.netflix.com/tr/"),
                                new CardAction(ActionTypes.OpenUrl, "Prime Video", value: "https://www.primevideo.com/"),
                                new CardAction(ActionTypes.OpenUrl, "BluTV", value: "https://www.blutv.com"),
                                new CardAction(ActionTypes.OpenUrl, "mubi", value: "https://mubi.com/tr"),
                                new CardAction(ActionTypes.OpenUrl, "Puhu TV", value: "https://puhutv.com/")
                            }
                        };
                        return heroCard.ToAttachment();
                    }
                    Activity reply = MessageFactory.Text("Film kanallarını inceleyebilirsiniz:");
                    reply.AttachmentLayout = AttachmentLayoutTypes.List;

                    reply.Attachments.Add(Film());

                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);
                }


                if (list[0].Contains("dizi"))
                {
                    Attachment Dizi()
                    {
                        var heroCard = new ThumbnailCard
                        {
                            Title = "Fun Bot",
                            Subtitle = "Dizi Platformları",
                            Text = "Platformlara aşağıdaki linklerden ulaşabilirsiniz.",
                            Images = new List<CardImage> { new CardImage("https://upload.wikimedia.org/wikipedia/tr/0/07/Do%C4%9Fu%C5%9F_Yay%C4%B1n_Grubu_logosu.png") },
                            Buttons = new List<CardAction> {
                                new CardAction(ActionTypes.Call, "Netflix", value: "https://www.netflix.com/tr/"),
                                new CardAction(ActionTypes.OpenUrl, "Prime Video", value: "https://www.primevideo.com/"),
                                new CardAction(ActionTypes.OpenUrl, "BluTV", value: "https://www.blutv.com"),
                                new CardAction(ActionTypes.OpenUrl, "Star TV", value: "https://startv.com.tr"),
                                new CardAction(ActionTypes.OpenUrl, "Puhu TV", value: "https://puhutv.com/")
                            }
                        };
                        return heroCard.ToAttachment();
                    }
                    Activity reply = MessageFactory.Text("Dizi kanallarını inceleyebilirsiniz:");
                    reply.AttachmentLayout = AttachmentLayoutTypes.List;

                    reply.Attachments.Add(Dizi());

                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);
                }

                if (list[0].Contains("Müzik"))
                {
                    Attachment Muzik()
                    {
                        var heroCard = new ThumbnailCard
                        {
                            Title = "Fun Bot",
                            Subtitle = "Müzik Platformları",
                            Text = "Sitelere aşağıdaki linklerden ulaşabilirsiniz.",
                            Images = new List<CardImage> { new CardImage("https://upload.wikimedia.org/wikipedia/tr/0/07/Do%C4%9Fu%C5%9F_Yay%C4%B1n_Grubu_logosu.png") },
                            Buttons = new List<CardAction> {
                                new CardAction(ActionTypes.OpenUrl, "YouTube", value: "https://www.youtube.com/"),
                                new CardAction(ActionTypes.OpenUrl, "Spotify", value: "https://www.spotify.com"),
                            }
                        };
                        return heroCard.ToAttachment();
                    }
                    Activity reply = MessageFactory.Text("Müzik kanallarını inceleyebilirsiniz:");
                    reply.AttachmentLayout = AttachmentLayoutTypes.List;

                    reply.Attachments.Add(Muzik());

                    await stepContext.Context.SendActivityAsync(reply, cancellationToken);
                }
            }

            // Create the list of options to choose from.
            var options = _companyOptions.ToList();
            options.Add(DoneOption);
            if (list.Count > 0)
            {
                options.Remove(list[0]);
            }

            var promptOptions = new PromptOptions
            {
                Prompt = MessageFactory.Text("Nasıl vakit geçirmek istiyorsun?"),
                RetryPrompt = MessageFactory.Text(""),
                Choices = ChoiceFactory.ToChoices(options),
            };

            // Prompt the user for a choice.
            return await stepContext.PromptAsync(nameof(ChoicePrompt), promptOptions, cancellationToken);
        }

        private async Task<DialogTurnResult> LoopStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            // Retrieve their selection list, the choice they made, and whether they chose to finish.
            var list = stepContext.Values[CompaniesSelected] as List<string>;
            var choice = (FoundChoice)stepContext.Result;
            var done = choice.Value == DoneOption;

            if (!done)
            {
                // If they chose a company, add it to the list.
                list.Add(choice.Value);
            }

            if (list.Count > 1)
            {
                list.Remove(list[0]);
            }

            if (done)
            {
                // If they're done, exit and return their list.
                return await stepContext.EndDialogAsync(list, cancellationToken);
            }
            else
            {
                // Otherwise, repeat this dialog, passing in the list from this iteration.
                return await stepContext.ReplaceDialogAsync(nameof(ReviewSelectionDialog), list, cancellationToken);
            }
        }
    }

}

