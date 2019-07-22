window.postContent = function(str, lang) {
    var req = new XMLHttpRequest();
    return new Promise(function(resolve, reject) {
        req.onreadystatechange = function () {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    var resJson = JSON.parse(this.response);
                    resolve(resJson);
                }
                else {
                    var resJson = JSON.parse(this.response);
                    reject(resJson);
                }
            }
        }

        req.open("POST", "api/upload", true);
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");

        var jsonPayload = {
            snippet: str,
            language: lang
        };

        req.send(JSON.stringify(jsonPayload));
    });
}

var lastHighlight;

window.highlightSelectedLine = function() {
    var hash = window.location.hash;
    if (!hash) return;

    if (lastHighlight)
        $(lastHighlight).removeClass('selected');

    var number = hash.substr(1, hash.length-1);
    lastHighlight = $(`.line-number #${number}`).first().parent();
    lastHighlight.addClass('selected');
}