Weather Service API
This is a simple API that provides weather data for a given location.

Prerequisites
.NET Core 3.1 or later
An API key from OpenWeatherMap
Configuration
Before running the API, you need to set your OpenWeatherMap API key in the appsettings.json file:

json


{
  "WeatherApi": {
    "ApiKey": "YOUR_API_KEY"
  }
}
Replace YOUR_API_KEY with your actual API key.

Running the API
To run the API, navigate to the project directory in your terminal and run the following command:

bash


dotnet run
This will start the API on http://localhost:5000.

Endpoints
The API has the following endpoint:

GET /weather?location={location}: Returns the weather data for the given location.
Testing
To run the unit tests, navigate to the test project directory in your terminal and run the following command:

bash


dotnet test

- Was it easy to complete the task using AI? not to easy
- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics) 3 hours
- Was the code ready to run after generation? What did you have to change to make it usable? add packages, references to libraries
- Which challenges did you face during completion of the task? It did not generate the complete code, but only pieces or examples.
- Which specific prompts you learned as a good practice to complete the task? put the error message to get a better solution.
