JsonResult is a set of classes that discuss with the server on what to do next:

One example:
Click on a button => send an ajax call 'button clicked'
The server tells the client to ReplaceMainHtml content with the result of an action
Therefore the client ask for the action to render and replace the main html by the response.

The Server can embeddd multiple actions to do with the ComposeResult class.