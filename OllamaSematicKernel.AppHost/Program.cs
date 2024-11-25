var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OllamaSemanticKernelApi>("ollamasemantickernelapi");

builder.Build().Run();
