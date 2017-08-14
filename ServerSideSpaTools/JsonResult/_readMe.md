# JsonResult is a set of classes to inform the client layer what to do after an ajax call.


One example:
Click on a button => send an ajax call 'button clicked'
The server reponds a ReplaceMainHtml content with the result of an action
Therefore the client will replace the main content by the reponse of the action to render.

One response can contain multiple commands withe ComposeResult class.
