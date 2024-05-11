# One Day: Mindfulness and Mood Tracking Platform

One Day is a digital companion for managing depression, engineered using Next.js for the frontend and .NET for the backend. 

Features at a Glance:
- Mindfulness Journal: Logs daily user entries, facilitating the identification and modification of thought patterns.
- Mood Tracker: Records and analyzes mood over time, presenting actionable insights and trends.

# Development Setup
To get started with local development:

```
dotnet user-secrets init
dotnet user-secrets set "MongoDB:URI" "mongodb://onedayuser:onedaypassword@localhost:27017/oneday"
dotnet user-secrets set "JWT:Key" "SharksLikeToParty!DidYouKnowSomeWearTieDyeInOceanCurrents?"
```
