(function() {

    this.userItems.forEach(item => {
        item.addEventListener(
            'click',
            event => addItemToTradeBag(event.currentTarget)
        )
    });


})();


function addItemToTradeBag(item) {
    this.userTradeBag.appendChild(item);

    item.addEventListener(
        'click',
        event => removeItemFromTradeBag(event.currentTarget)
    );
}


function removeItemFromTradeBag(item) {
    this.userBag.appendChild(item);

    item.addEventListener(
        'click',
        event => addItemToTradeBag(event.currentTarget)
    );
}


// function addItemToTradeBag(item) {
//     let tradeItemId = PREFIX_TRADE_BAG + item.id;
//
//     if (window.userTradeItemsIds.includes(tradeItemId)) {
//         let pendingItem = document.getElementById(tradeItemId);
//         let counter = pendingItem.getElementsByClassName(CLASS_INVENTORY_ITEM_COUNTER)[0];
//         counter.textContent = parseInt(counter.textContent, 10) + 1;
//
//     } else {
//         let cloned = item.cloneNode(true);
//         let counter = cloned.getElementsByClassName(CLASS_INVENTORY_ITEM_COUNTER)[0];
//         cloned.id = PREFIX_TRADE_BAG + item.id;
//         counter.textContent = 1;
//
//         cloned.addEventListener(
//             'click',
//             event => removeItemFromTradeBag(event.currentTarget)
//         );
//
//         this.userTradeBag.appendChild(cloned);
//     }
//
//     let counter = item.getElementsByClassName(CLASS_INVENTORY_ITEM_COUNTER)[0];
//     counter.textContent = parseInt(counter.textContent, 10) - 1;
//     window.userTradeItemsIds.push(tradeItemId);
// }
//
//
// function removeItemFromTradeBag(item) {
//     let counter = item.getElementsByClassName(CLASS_INVENTORY_ITEM_COUNTER)[0];
//     let nextCounterValue = parseInt(counter.textContent, 10) - 1;
//
//     if (nextCounterValue === 0) {
//         item.parentNode.removeChild(item);
//     } else {
//         counter.textContent = parseInt(counter.textContent, 10) - 1;
//     }
//
//     let inventoryItemId = item.id.split(PREFIX_SPLITTER).pop();
//     let inventoryItem = document.getElementById(inventoryItemId);
//     counter = inventoryItem.getElementsByClassName(CLASS_INVENTORY_ITEM_COUNTER)[0];
//     counter.textContent = parseInt(counter.textContent, 10) + 1;
//
//     window.userTradeItemsIds.splice(window.userTradeItemsIds.indexOf(item.id), 1);
// }
