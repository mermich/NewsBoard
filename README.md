# NewsBoard
This app is a community feed agregator.

## Projects organization
- NewsBoard folder contains the main web project.
- NewBoardRestApi folder contains the webapi project that implements the business logic.
- 'ext/DiscoverWebSite' is a small project to browse and parse a webpage in order to discover the tss/atom feeds.
- 'ext/ext/ApiTools' contains tools to build restapi.
- 'ext/ext/ServerSideSpaTools' contains tools to build a single page app.

#### Current features
- Any registered user can add need feeds and subscribe to them.
- Any registered user can subscribe to feeds.
- Any registered user can display their personnal subscription.
- Any registered user can report any feed.
- Any user can browse the list of feed.
- Any user can browse the articles of any feed.
- Admins can edit feed tags.
- Feeds can be filtered by tags.


#### To Fix
- Move database connection string to the config

