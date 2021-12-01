this.userBag = null;
this.userTradeBag = null;
this.userItems = null;
this.siteBag = null;
this.siteTradeBag = null;
this.siteItems = null;

this.userTradeItemsIds = [];
this.siteTradeItemsIds = [];


(function() {

    bags = [...document.getElementsByClassName(CLASS_INVENTORY_BAG)];
    bags.forEach(bag => { if (bag.getAttribute(OWNER) === USER) window.userBag = bag; });
    bags.forEach(bag => { if (bag.getAttribute(OWNER) === SITE) window.siteBag = bag; });

    userItems = [...userBag.getElementsByClassName(CLASS_INVENTORY_ITEM)];
    siteItems = [...siteBag.getElementsByClassName(CLASS_INVENTORY_ITEM)];

    tradeBags = [...document.getElementsByClassName(CLASS_TRADE_BAG)];
    tradeBags.forEach(bag => { if (bag.getAttribute(OWNER) === USER) window.userTradeBag = bag; });
    tradeBags.forEach(bag => { if (bag.getAttribute(OWNER) === SITE) window.siteTradeBag = bag; });

})();
