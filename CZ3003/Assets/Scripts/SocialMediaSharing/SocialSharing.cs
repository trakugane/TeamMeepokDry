using UnityEngine;
using System.Collections;

public class SocialSharing : MonoBehaviour
{

	//Twitter variables
	string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	string TWEET_LANGUAGE = "en";
    string textToDisplay = "Hey Guys! Check out my score: ";



	//App ID
	string AppID = "805120583426252";

	//This link is attached to this post
	string Link = "https://www.facebook.com/TeamMeePokDry/about/?ref=page_internal";

	//The URL of a picture attached to this post. 
	//The Size must be atleat 200px by 200px.
	string Picture = "http://i-cdn.phonearena.com/images/article/85835-thumb/Google-Pixel-3-codenamed-Bison-to-be-powered-by-Andromeda-OS.jpg";

	string Caption = "Check out My New Score: ";
	string Description = "Enjoy Fun, free games! Challenge yourself or share with friends. Fun and easy to use games.";


	// Twitter Share Button	
	public void shareScoreOnTwitter()
	{

		//Can modify by level or assignment
		string nameParameter = "ASSIGNMENT COMPLETED!";//this is limited in text length 
		Application.OpenURL(TWITTER_ADDRESS +
		   "?text=" + WWW.EscapeURL(textToDisplay + "\n\nGet the Game:\n" + Link + "\n\n#TeamMeePokDryGame #CZ3003"));

	}

	// Facebook Share Button
	public void shareScoreOnFacebook()
	{
		Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture
							 + "&caption=" + Caption /*+ score.points*/ + "&description=" + Description);
	}

	string text = "I have created a PVP room. \n\nChallenge me at " + PhotonLobbyCustom.lobby.roomName;
	public void shareRoomOnTwitter()
    {
		//string text = "I have created a PVP room. \n\nChallenge me at " + PhotonLobbyCustom.lobby.roomName;
		Application.OpenURL(TWITTER_ADDRESS +
		   "?text=" + WWW.EscapeURL(text + "\n\nGet the Game:\n" + Link + "\n\n#TeamMeePokDryGame #CZ3003"));
	}

	public void shareRoomOnFacebook()
    {
		Application.OpenURL("https://www.facebook.com/dialog/feed?" + "app_id=" + AppID + "&link=" + Link + "&picture=" + Picture
							 + "&caption=" + text);
	}
}
