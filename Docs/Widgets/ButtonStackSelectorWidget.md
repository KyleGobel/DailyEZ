Button Stack Selector Widget
========================

Getting Started
--------------
The Button Stack Selector Widget has 3 javascript dependencies: jquery.js, jquery.ba-bbq.js, and some AMD module loader (requirejs).  It is an AMD module made with Typescript, so it doesn't mention its dependencies on jquery and jquery.ba-bbq in it's define.  The host page must also know where the root of the application is (relative to the host page).  This is so it can find where the widgets directory is.

```
<link rel="home" id="ApplicationRoot" href="../../" />
```
This is an example of what you would add to the host page if you were 2 directories away from the root of the application.

If you had a directory structure like

    root/
       widgets/
           ButtonStackSelector/
       someDataFolder/
           moreData/
               samplePage.html
               jquery.js
               jquery.ba-bbq.js
               require.js
               widgets.css
               dailyEZBootstrap.css

Then a basic html page would look like this
**samplePage.html**

```
<!DOCTYPE html>
<html>
    <head>
        <!--Define Application Root-->
        <link rel="home" id="ApplicationRoot" href="../../" />
        <!--Import StyleSheets-->
        <link rel="stylesheet" href="widgets.css" />
        <link rel="stylesheet" href="dailyEZBootstrap.css"/>
        
        <!--Load up Javascript dependencies-->
        <script src="jquery-1.9.1.min.js"></script>
        <script src="jquery.ba-bbq.js"></script>
        <script src="require.js"></script>
    </head>
    <body>
        <script>
            require(['../ButtonStackSelector/buttonStackSelector.js'],
            function(widgetLibrary) {
                var w = new widgetLibrary.Widgets.ButtonStackSelector();
                w.title = "Button Stack Selector Example";
                w.widgetId = 1;
                w.stacks = "220, 221, 219, 213, 214, 212, 216, 217";
                w.buttonColor = 'btn-info';
                w.run(null);
              
            });

        </script>
        <div id="dailyEZ-com-button-stack-selector-widget1">hi</div>
    </body>
</html>
```

Properties
----------
The Button Stack Selector widget has 4 propertes

 - **title**
	
	Title is the main text you see above the widget


 - **widgetId**

	WidgetId is the Identifier of where the widget should display on the host page.  The widget will look for a DOM id of `dailyEZ-com-button-stack-selector-widget[widgetId]` and that's where the widget will be rendered

 - **stacks**

	stacks is a `string` list of stacks, seperated by commas.  Each entry should coorelate to a stackId that has been already created

	
 - **buttonColor**

	the css class of the button color to use.  This is one of the reasons the bootstrap css is included, there are also additional css button color values located in `widgets.css`


Usage
-------
The Button Stack Selector only exposes one method, that is `run` run takes one optional paramater which is `callback(success: boolean)` that will be called when the run is completed, with the first parameter being a boolean that is true if everything went smoothly, or false if somewhere along the line there was a failure.

`run` should be called after you have set the initial properties




