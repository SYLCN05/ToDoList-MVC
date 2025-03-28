using ToDoList.Interfaces;

namespace ToDoList.Services
{
    public static  class ThreadPoolConfig
    {
    public static void ConfigureThreadPool(IServiceCollection services)
    {

        // Thread Pool configuratie
        int minWorkerThreads = Environment.ProcessorCount * 2; // bekijken van de minimaal aantal worker threads
        int minCompletionPortThreads = Environment.ProcessorCount;
        int maxWorkerThreads = 100; // bekijken van de maximaal aantal worker threads
        int maxCompletionPortThreads = 100; 

        ThreadPool.SetMinThreads(minWorkerThreads, minCompletionPortThreads); // ervoor zorgen dat er altijd een minimaal aantal threads beshcikbaar zijn voor taken
        ThreadPool.SetMaxThreads(maxWorkerThreads, maxCompletionPortThreads); // beperk het aantal threads om belasting te voorkomen
    }
}
}
