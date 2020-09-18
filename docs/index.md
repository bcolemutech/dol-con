# Dominion of Light

![PR Verify](https://github.com/bcolemutech/dol-con/workflows/PR%20Verify/badge.svg)

![Release](https://github.com/bcolemutech/dol-con/workflows/Release/badge.svg)

This is the start of a great journey. The creation of Dominion of Light the game.
This game is a Google Cloud hosted world. The first phase of the game is a text adventure.
As the game world evolves new features will be released to the app.
The text app will be used to build out the SDK for future phases (Unity).

## Mission

Although this project may never get truly released but there are many other goals I wish to achieve
with this project.

- **Unlimited game play** - This is at the core of the games design
- Unlimited character building
  - Skill progression based on how you play not a point system
- Massive open world
  - Improbable to explore entirely
  - Persistance
  - Never exactly the same when revisiting
  - Evolving with AI based factions that govern everything even nature
- New quests all the time that evolve with the world
  - Minor quest opportunities everywhere
  - Full campaigns form automatically
  - No static story lines
- Tell the Dominion of Light story
  - This world has been designed, refactored, and grown for over 10 years.
  - It is my favorite creation
- Further improve cloud development skills
  - The game world will live in the cloud
  - Secure logins will be required for all users
  - Single API
  - Event driven workers for more intense processing
  - Possible use of AI to drive faction work loads

## How to play

### Get the app (text adventure)

#### Download the app from the site

Use the link at the top of the screen to download for PC or OSX

**OSX** - Tested on macOS 10.15 Catalina

**PC** - Tested on Windows 10 x64

#### Clone and build it yourself

The NET Core 3.1 app can be pulled from the public repo [dol-col](https://github.com/bcolemutech/dol-con).

### Get access

At this time I will only give access to a small group.
Eventually I will grant access to larger groups as the releases become more feature rich and stable.

## Third Party Resources

- [Google Cloud Platform](https://cloud.google.com/)
  - [Source Repositories](https://source.cloud.google.com/)
  - [Cloud Build](https://cloud.google.com/cloud-build)
  - [Cloud Run](https://cloud.google.com/run)
  - [Firestore](https://cloud.google.com/firestore)
  - [Identity Platform](https://cloud.google.com/identity-platform)
  - [Firebase](https://firebase.google.com/)
- [Rider](https://www.jetbrains.com/rider/)
- [VS Code](https://code.visualstudio.com/)
- [GitHub](https://github.com/)
- [Azgaar's Fantasy Map Generator](https://azgaar.github.io/Fantasy-Map-Generator/)

## Road Map

The Road map included 3 phases.
First the text adventure (Version Zero). This is a test driven PC/Mac console app to help build out the SDK and cloud environment.
Next will be a simple Unity based mobile game that will implement the SDK (Version One).
The final phase may either be a Steam Greenlight project or an improved mobile game.

The following is the current road map subject to change.

| Feature                     | Release                 | Cloud | Text | Unity Mobile | Unity (Greenlight or Mobile V2) |
| --------------------------- | ----------------------- | ----- | ---- | ------------ | ------------------------------- |
| Login                       | [0.0](Releases/v0-0.md) | X     | X    |              |                                 |
| Navigate the game world     | 0.1                     | X     | X    |              |                                 |
| Inventory/Skills            | 0.2                     | X     | X    |              |                                 |
| Encounters and Combat       | 0.3                     | X     | X    |              |                                 |
| Quests                      | 0.4                     | X     | X    |              |                                 |
| Campaigns                   | 0.5                     | X     | X    |              |                                 |
| Factions                    | 0.6                     | X     | X    |              |                                 |
| **Alpha Release**           | 0.6                     |       | X    |              |                                 |
| Login                       | 1.0                     |       |      | X            |                                 |
| Navigate the game world     | 1.1                     |       |      | X            |                                 |
| Inventory/Skills            | 1.2                     |       |      | X            |                                 |
| Encounters and Combat       | 1.3                     |       |      | X            |                                 |
| Quests                      | 1.4                     |       |      | X            |                                 |
| Campaigns                   | 1.5                     |       |      | X            |                                 |
| Factions                    | 1.6                     |       |      | X            |                                 |
| **Beta Release**            | 1.6                     |       |      | X            |                                 |
| Login                       | 2.0                     |       |      |              | ?                               |
| Navigate the game world     | 2.1                     |       |      |              | ?                               |
| Inventory/Skills            | 2.2                     |       |      |              | ?                               |
| Encounters and Combat       | 2.3                     |       |      |              | ?                               |
| Quests                      | 2.4                     |       |      |              | ?                               |
| Campaigns                   | 2.5                     |       |      |              | ?                               |
| Factions                    | 2.6                     |       |      |              | ?                               |
| **Greenlight Beta Release** | 2.6                     |       |      |              | ?                               |
