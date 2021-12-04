$('.dropdown-trigger').dropdown();
$(document).ready(function () {
    $('.modal').modal();
});
$('.sidenav').sidenav({
    menuWidth: 300, // Default is 240
    closeOnClick: true // Closes side-nav on <a> clicks, useful for Angular/Meteor
});
$('.collapsible').collapsible();

function copyToClipboard(element, cb) {
    var text = $(element).text();
    if (text === '') text = $(element).val();
    navigator.clipboard.writeText(text).then(function() {
        console.log('Async: Copying to clipboard was successful!');
    }, function(err) {
        console.error('Async: Could not copy text: ', err);
    });
    if (cb) {
        cb();
    }
}

$(document).on('click', '.copy_click', function() {
    var self = $(this);
    var curr_html = $(this).html();
    var element = $(this).attr('elem');
    var copied = $(this).attr('copied');
    copyToClipboard(element, function() {
        if (copied == 'true') {
            $(self).html('Copied');
            setTimeout(function() {
                $(self).html(curr_html);
            }, 1000)
        }
    });
})