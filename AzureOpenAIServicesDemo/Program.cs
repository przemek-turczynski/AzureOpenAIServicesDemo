using Azure.AI.OpenAI;
using Azure;

namespace AzureOpenAIServicesDemo;

internal class Program
{
    // Define parameters and initialize the client
    private static readonly string Endpoint = "https://openai-pt.openai.azure.com/";
    private static readonly string Key = "fa3ff0d5cc5942f888358d89ef31ecbb";

    static void Main(string[] args)
    {
        GetChatCompletionsDemo();
        GetCompletionsDemo();
        GetEmbeddingDemo();
    }

    private static void GetChatCompletionsDemo()
    {
        OpenAIClient client = new OpenAIClient(new Uri(Endpoint), new AzureKeyCredential(Key));

        // Build completion options object
        ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
        {
            Messages =
            {
                new ChatRequestSystemMessage("You are a helpful AI bot."),
                new ChatRequestUserMessage("What is Azure OpenAI?"),
            },
            DeploymentName = "gpt-35-turbo-pt",
            MaxTokens = 400,
            Temperature = 0.7f
        };

        // Send request to Azure OpenAI model
        ChatCompletions response = client.GetChatCompletions(chatCompletionsOptions);

        // Print the response
        string completion = response.Choices[0].Message.Content;
        Console.WriteLine("Response:\n" + completion + "\n");
    }

    private static void GetCompletionsDemo()
    {
        OpenAIClient client = new OpenAIClient(new Uri(Endpoint), new AzureKeyCredential(Key));

        // Build completion options object
        CompletionsOptions completionsOptions = new CompletionsOptions()
        {
            
            DeploymentName = "gpt-35-turbo-pt",
            MaxTokens = 50,
            Temperature = 0.7f,
            Prompts = { "ChatCompletions response =" }
        };

        // Send request to Azure OpenAI model
        var response = client.GetCompletions(completionsOptions);

        // Print the response
        for (int i = 1; i <= response.Value.Choices.Count; i++)
        {
            Console.WriteLine($"Response {i}\n: {response.Value.Choices[i - 1].Text}");
        }
    }

    private static void GetEmbeddingDemo()
    {
        OpenAIClient client = new OpenAIClient(new Uri(Endpoint), new AzureKeyCredential(Key));

        // Build embeddings options object
        EmbeddingsOptions embeddingOptions = new()
        {
            DeploymentName = "text-embedding-ada-002-pt",
            Input = { "What is Azure OpenAI?" },
        };

        // Send request to Azure OpenAI model
        var response = client.GetEmbeddings(embeddingOptions);

        // Print the response
        foreach (float item in response.Value.Data[0].Embedding.ToArray())
        {
            Console.WriteLine(item);
        }
    }
}
