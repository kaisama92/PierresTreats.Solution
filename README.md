# _Pierre's Treats_

#### By _**Kai Clausen**_

#### _This application will keep track of engineers and machines in a factory. Built for independent review._

## Technologies Used

* _C#_
* _.Net_
* _HTML_
* _CSS_
* _MySQL_
* _MySQL Workbench_
* _Bootstrap (button styling)_

## Description

_This application was created as an independent project to be submitted for code review to demonstrate an understanding of Many-To-Many databases. The styling was not a priority._

## Setup/Installation Requirements

* _If you do not have C#/.Net, dotnet-script, and MySQL, visit [this page](https://www.learnhowtoprogram.com/c-and-net) and work through the second Pre-Work lesson_
* _Once C#/.Net, dotnet-script, and MySQL are correctly installed and configured, clone down this repository_
* _Navigate to the production directory, labeled ```Factory```_
* _Once there, in your terminal enter the command ```dotnet tool install --global dotnet-ef --version 6.0.0```_
* _When that is finished enter ```dotnet add package Microsoft.EntityFrameworkCore.Design -v 6.0.0```_
* _Then you will create a new file within the ```Factory``` directory_
* _Name the file ```appsettings.json```_
* _Copy and paste the following into this file_

```cs
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DB-NAME];uid=[YOUR-USER-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}
```

* _Replace [YOUR-DB-NAME] with a name of your choice, [YOUR-USER-HERE] with your userid, and [YOUR-PASSWORD-HERE] with your password_
* _In the top level of the ```Factory``` directory, enter the command ```dotnet ef database update``` to create a database based on the existing migrations._
* _Once you have completed the above instructions, you can enter the command in the terminal ```dotnet run``` to launch the application in your browser._
* _When you are finished, exit the browser window and click into your terminal and press Ctrl+C_

## Known Bugs

* _No known bugs_

## License

_MIT - If you encounter any bugs, please feel free to fix them yourself, or contact me at kaiclausen123@gmail.com_

Copyright (c) _2023_ _Kai Clausen_