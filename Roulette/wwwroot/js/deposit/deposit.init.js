this.userBag = null;
this.userTradeBag = null;
this.userItems = null;
this.siteBag = null;
this.siteTradeBag = null;
this.siteItems = null;

this.userTradeItemsIds = [];
this.siteTradeItemsIds = [];


window.InitDeposit = () =>  {

    bags = [...document.getElementsByClassName(CLASS_INVENTORY_BAG)];
    bags.forEach(bag => { if (bag.getAttribute(OWNER) === USER) window.userBag = bag; });
    bags.forEach(bag => { if (bag.getAttribute(OWNER) === SITE) window.siteBag = bag; });

    userItems = [...userBag.getElementsByClassName(CLASS_INVENTORY_ITEM)];
    siteItems = [...siteBag.getElementsByClassName(CLASS_INVENTORY_ITEM)];

    tradeBags = [...document.getElementsByClassName(CLASS_TRADE_BAG)];
    tradeBags.forEach(bag => { if (bag.getAttribute(OWNER) === USER) window.userTradeBag = bag; });
    tradeBags.forEach(bag => { if (bag.getAttribute(OWNER) === SITE) window.siteTradeBag = bag; });

};

window.getSelectedItems = () => {
    let children = document.getElementById("userTradeBag").children;

    let idArr = [];
    for (let i = 0; i < children.length; i++) {
        idArr.push(children[i].id);
    }

    children = document.getElementById("siteTradeBag").children;

    let siteIdArr = [];
    for (let i = 0; i < children.length; i++) {
        siteIdArr.push(children[i].id);
    }
    if(siteIdArr.length !==0 && idArr.length !== 0 || siteIdArr.length === 0 && idArr.length === 0){
        return "";
    }
    let inv = idArr;
    let invName = 'user';
    if (inv.length === 0){
        inv = siteIdArr;
        invName = 'site';
    }
    let data = {inv, invName}
    return JSON.stringify(data)
};