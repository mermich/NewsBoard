# NewsBoard
This app is a community feed agregator. 
- As a user you can add feeds that youll subscribe. 
- You can also directly paset a website adress, and the engine will try to find any feed to subscribe
- On top of it the app can suggest you feed used from other users that you can subscribe.
- No personall info are gathered, no ads, all clean !

## Projects organization
- NewsBoard folder contains the main web project.
- NewBoardRestApi folder contains the webapi project that implements the business logic.
- ApiTools contains tools to build restapi.
- ServerSideSpaTools contains tools to build a single page app.

#### Current features
- Any registered user can add need feeds and subscribe to them.
- Any registered user can subscribe to feeds.
- Any registered user can display their personnal subscription.
- Any registered user can report any feed.
- Any user can browse the list of feed.
- Any user can browse the articles of any feed.
- Admins can edit feed tags.
- Feeds can be filtered by tags.


#### To Do
- Automaticly refresh feeds on a daily basics ?
- Chrome extention, or snippet ?
- Add a Friends feature
- Display feeds of friends
- Suggest feeds better
- Revamp home page to insists on community
- Prevent from spamming
- ApiTools and ServerSideSpaTools should be refactored into something else (smaller)
- Merge code into a fluent api ?
- Immuatability ?
