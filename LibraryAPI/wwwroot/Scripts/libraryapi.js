/****************************************************************************
         * Add a new TodoItem.
         *
         * 1) send an update to the DB
         * 2) if successful then add the item to the list
         ****************************************************************************/
function addNewLibraryItem() {

    // Get the value from the Input field in the FORM
    let itemNameValue = document.getElementById("newItemName").value.trim(); //declaring a variable and reading the value in the textbox
    let itemTypeValue = document.getElementById("newItemType").value.trim();
    let statusValue = document.getElementById("newStatus").value.trim();

    // Check that a value have added
    if (itemNameValue === "" || itemTypeValue === "" || statusValue === "") {
        alert("Please enter a value for your item");
    }
    createLibraryItem(itemNameValue, itemTypeValue, statusValue);
    document.getElementById("newItemName").value = "";
    document.getElementById("newItemType").value = "";
    document.getElementById("newStatus").value = "";
}

//Update:
function getUpdatedLibraryForm() {

    // Get the value from the Input field in the FORM
    let itemIdValue = document.getElementById("updateLibraryID").value.trim();
    let itemNameValue = document.getElementById("updateLibraryItemName").value.trim(); //declaring a variable and reading the value in the textbox
    let itemTypeValue = document.getElementById("updateLibraryItemType").value.trim();
    let statusValue = document.getElementById("updateLibraryItemStatus").value.trim();

    
    updateLibraryItem(itemIdValue, itemNameValue, itemTypeValue, statusValue);
    //ToDo: JavaScript to update list item 
    
}

/****************************************************************************
 * This function will add the a new todo item to the UL element
 * Specifically this will add:
 *
 *   <li>the item description<span class="close">X</>li>
 *
 * 1) add to DB
 * 2) if successful then add the item to the list
 *
 ****************************************************************************/
function addLibraryItemToDisplay(item) {
    let libraryItemNode = document.createElement("li"); //document is the object which creates the HTML, createElement creates <li></li> and allows you to add things below it
    let itemNameNode = document.createTextNode(item["itemName"]);
    let itemTypeNode = document.createTextNode(item["itemType"]);
    let statusNode = document.createTextNode(item["status"]);
    libraryItemNode.append(itemNameNode, ",", " ", itemTypeNode, ", ", " ", statusNode);

    document.getElementById("todoList").appendChild(libraryItemNode);

    //ToDo: Repeat lines 50-56 = Need to find it, then delete it and then recreate it with the updated value

    let tickSpanNode = document.createElement("SPAN");
    let tickText = document.createTextNode("\u2713");  // \u2713 is unicode for the tick symbol
    tickSpanNode.appendChild(tickText);
    libraryItemNode.appendChild(tickSpanNode);
    tickSpanNode.className = "tickHidden";

    let closeSpanNode = document.createElement("SPAN"); // Creating a text node with a SPAN
    let closeText = document.createTextNode("X"); // Creating a text node with a SPAN
    closeSpanNode.className = "close";
    closeSpanNode.appendChild(closeText);
    libraryItemNode.appendChild(closeSpanNode);

    closeSpanNode.onclick = function (event) {
        // When the use press the "X" button, the click event is normally also passed to its parent element.
        // (i.e. the element containing the <SPAN>). In the case the LI element that is holding the TodoItem
        // which would have resulted in a toggle of item between "DONE" and "NEW"
        //
        // stopPropagation() tells the event not to propagate
        event.stopPropagation();

        if (confirm("Are you sure that you want to delete this item?")) {
            deleteLibraryItem(item["libraryItemId"]);

            // Remove the HTML list element that is holding this todo item
            libraryItemNode.remove();
        }
    }

    libraryItemNode.onclick = function () {

        document.getElementById("updateLibraryID").value = item["libraryItemId"]; //On click we know what we are
        document.getElementById("updateLibraryItemName").value = item["itemName"];
        document.getElementById("updateLibraryItemType").value = item["itemType"];
        document.getElementById("updateLibraryItemStatus").value = item["status"];

        //ToDo: Find out how to indicate which row is selected
        libraryItemNode.classList.toggle("checked");
        tickSpanNode.classList.toggle("tickVisible");
   
    }

   
}

/****************************************************************************
 * CRUD functions calling the REST API
 ****************************************************************************/

function createLibraryItem(newItemName, newItemType, newStatus) {

    // Create a new JSON object for the new item with the status of NEW
    // Since the id is generated by the microservice, we will use -1 as a dummy
    // If the POST is successful the microservice will store the new item in the database
    // and returns a JSON via the response with the generated id for the new item
    const newItem = {
        "itemName": newItemName, "itemType": newItemType, "status": newStatus
    };
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newItem)
    };
    fetch('https://localhost:7227/LibraryItem', requestOptions)
        // get the JSON content from the response
        .then((response) => {
            if (!response.ok) {
                alert("An error has occurred.  Unable to create the TODO item")
                throw response.status;
            } else return response.json();
        })

        // add the item to the UL element so that it will appear in the browser
        .then(item => addLibraryItemToDisplay(item));
}

// Load the list - expecting an array of todo_items to be returned
function readLibraryItems() {
    fetch('https://localhost:7227/LibraryItem')
        // get the JSON content from the response
        .then((response) => {
            if (!response.ok) {
                alert("An error has occurred.  Unable to read the TODO list")
                throw response.status;
            } else return response.json();
        })
        // Add the items to the UL element so that it can be seen
        // As items is an array, we will the array.map function to through the array and add item to the UL element
        // for display
        .then(items => items.map(item => addLibraryItemToDisplay(item)));
}

function updateLibraryItem(itemId, itemName, itemType, status) {
    const updateItem = {
       "libraryItemId": itemId, "itemName":  itemName, "itemType": itemType, "status": status
    };
    const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updateItem)
    };
    fetch('https://localhost:7227/LibraryItem/' + itemId, requestOptions)
        .then((response) => {
            if (!response.ok) {
                alert("An error has occurred.  Unable to UPDATE the Library item")
                throw response.status;
            } else return response.json();
        })
}

function deleteLibraryItem(todoItemId) {
    fetch("https://localhost:7227/LibraryItem/" + todoItemId, { method: 'DELETE' })
        .then((response) => {
            if (!response.ok) {
                alert("An error has occurred.  Unable to DELETE the TODO item")
                throw response.status;
            } else return response.json();
        })
}

function DisplayJoinedBooks() {
    fetch('https://localhost:7227/User/info')
        .then(function (response) {
            return response.json();
        })
        .then(function (data) {
            appendData(data);
        })
};

function appendData(data) {
    var mainContainer = document.getElementById("showBooks");
    for (var i = 0; i < data.length; i++) {
        var div = document.createElement("div");
        div.innerHTML = 'ID: ' + data[i].id + '- Name: ' + data[i].name + ', Author: ' + data[i].author + ', Genre: ' + data[i].genre;;
        mainContainer.appendChild(div);
    }
}
