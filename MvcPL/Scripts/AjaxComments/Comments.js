$(document).ready(function () {
    $('#commentForm').submit(function (event) {
        event.preventDefault();
        var data = $(this).serialize();
        var url = $(this).attr('action');
        $.getJSON(url, data, function (comment) {
            $('#commentTemplate')
                .tmpl(comment)
                .prependTo('#comments');
        });
    });
});