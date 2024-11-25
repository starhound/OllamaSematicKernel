using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var kernel = Kernel.CreateBuilder()
    .AddWeaviateVectorStore(options: new()
    {
        Endpoint = new Uri("http://192.168.0.95:8080/v1/")
    })
    .AddOllamaChatCompletion(
        modelId: "llama3.2", 
        endpoint: new Uri("http://192.168.0.127:11434"))
    .Build();
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

var chatHistory = new ChatHistory();

var aiChatService = kernel.GetRequiredService<IChatCompletionService>();

while (true)
{
    Console.Write("Q > ");
    var input = Console.ReadLine();
    chatHistory.Add(new ChatMessageContent(AuthorRole.User, input));
    var response = "";
    await foreach (var item in aiChatService.GetStreamingChatMessageContentsAsync(chatHistory))
    {
        Console.Write(item.Content);
        response += item.Content;
    }
    chatHistory.Add(new ChatMessageContent(AuthorRole.Assistant, response));
    Console.WriteLine();
}
