using System;
using UnityEngine;
using UnityEngine.Networking;

public class RandomNumberGenerator
{
    private const string apiUrl = "https://www.randomnumberapi.com/api/v1.0/random?min=1&max=7&count=1";

    public void FetchRandomNumber(Action<int> onRandomNumberReceived, Action<string> onError)
    {
        // Create a UnityWebRequest to make a GET request to the API.
        UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);

        // Attach a completion callback to handle the result of the web request.
        webRequest.SendWebRequest().completed += (asyncOperation) =>
        {
            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Handle connection or protocol errors and invoke the error callback.
                Debug.LogError("Error: " + webRequest.error);
                onError?.Invoke(webRequest.error);
            }
            else
            {
                // Parse the JSON response and extract the integer value.
                string jsonResponse = webRequest.downloadHandler.text;
                int randomValue;

                if (TryParseRandomValue(jsonResponse, out randomValue))
                {
                    // Invoke the callback with the parsed random value.
                    onRandomNumberReceived?.Invoke(randomValue);
                }
                else
                {
                    // Handle parsing errors and invoke the error callback.
                    string errorMessage = "Failed to parse the random value from the response: " + jsonResponse;
                    Debug.LogError(errorMessage);
                    onError?.Invoke(errorMessage);
                }
            }
        };
    }

    private bool TryParseRandomValue(string response, out int value)
    {
        value = 0;

        // Remove brackets and newline characters from the response.
        response = response.Replace("[", "").Replace("]", "").Replace("\n", "");

        // Try to parse the remaining string as an integer.
        return int.TryParse(response, out value);
    }
}
