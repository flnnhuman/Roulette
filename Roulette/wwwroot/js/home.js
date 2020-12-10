$('.dropdown-trigger').dropdown();
$(document).ready(function(){
    $('.modal').modal();
});
$('.sidenav').sidenav({
    menuWidth: 300, // Default is 240
    closeOnClick: true // Closes side-nav on <a> clicks, useful for Angular/Meteor
});
$('.collapsible').collapsible();