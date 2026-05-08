Violation 1:

    1. Which SOLID principle is violated: Dependency Inversion

    2. Where in the code: In Program.cs it was missing dependency injection, in GradeController it had a 
        constructor injection failure 

    3. Why is it a violation: Even though the GradeController correctly depends on the interface not on the 
        repository, when the IDE made the GET request from launchSetting.json it threw a runtime exception 
        (InvalidOperationException). This happens because the DI container couldn't find what the controller
        asked for in the constructor.

    4. The fix: I registered the required dependency in Program.cs by adding: 
        builder.Services.AddScoped<IGradeReader, GradeRepository>();. This ensures a loose coupling so when the 
        repository is changed the controller requires zero modifications.  
        Also after moving the business logic into the service layer and injecting the GradeServie into the controller
        I had to register the service dependency in Program.cs.
        Another thing is that by doing this we solve the Open/Closed principle violation that could appear if we change 
        the source of the data like in task 4. 


Violation 2:

    1. Which SOLID principle is violated: Single Responsibility

    2. Where in the code: In GradeController.cs in functions GetAll() and GetById()

    3. Why is it a violation: Controllers have one job, to handle HTTP requests and returning HTTP responses,
        however these methods mix business logic/logging with routing (calculating the average:  gradeList.Average(i => i.Value) : 0)
        and logging int othe system console using Console.WriteLine.

    4. The fix: I created a new class in Services layer called GradeService that will handle the business logic. The controller 
        now must only call the service methods then wrap the result in a HTTP 200OK reponse. Also I removed the logging from those two
        if statements in the GetById() method.