(function ($) {
    // Serialize a form, surely coming from: https://jsfiddle.net/gabrieleromanato/bynaK/
    $.fn.serializeFormJSON = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
})(jQuery);

// On history.back
window.onpopstate = function (event) {
    $.get(event.state.url, function (r) {
        $("#page").html(r).hide().fadeIn(1000);
        document.body.scrollTop = document.documentElement.scrollTop = 0;
    });
}

$(function () {
    LoadCallback();
    history.pushState({ url: location.href }, "replaceHtml", location.href);

    $.blockUI.defaults.baseZ = 2000;
    $.blockUI.defaults.message = null;
});

function LoadCallback() {
    $("[ns-loader-url]").each(function (e) {
        let thisdiv = $(this);
        let url = $(this).attr("ns-loader-url");
        $(this).removeAttr("ns-loader-url");

        $.get(url, function (partial) {
            thisdiv.replaceWith(partial);
            LoadCallback();
        });
    });

    // Hook to a jquery click on ns-action-type='simpleGetAction' .
    $("[ns-action-type='simpleGetAction']").not("[ns-action='initialized']").each(function () {
        performSimpleAjaxAction(this, "GET");
    });

    // Hook to a jquery click on ns-action-type='simplePostAction' .
    $("[ns-action-type='simplePostAction']").not("[ns-action='initialized']").each(function () {
        performSimpleAjaxAction(this, "POST");
    });

    // Hook to a jquery click on ns-action-type='dataChanged' .
    $("[ns-action-type='dataChanged']").not("[ns-action='initialized']").each(function () {
        performSimpleAjaxAction(this, "POST");
    });

    function performSimpleAjaxAction(element, verb) {
        element.setAttribute("ns-action", "initialized");

        $(element).off('click').click(function (e) {
            let target = e.target;
            let $target = $(e.target);

            disableElement(target);

            var dataToSend = "";

            if (verb == "POST") {
                dataToSend = $target.closest("form").serializeFormJSON();
            }




            if ($target.is("i")) {
                // We could have clicked the icon <i> element. Should be refactored.
                target = $target.parent().first()[0];
            }
            else if ($target.is("img")) {
                // We could have clicked the icon <img> element. Should be refactored.
                target = $target.parent()[0];
            }


            let targetUrl = target.getAttribute("href");

            console.log('performSimpleAjaxAction: ' + targetUrl);
            
            // Display a loader.
            //$target.addClass('auto-loader');

            $.ajax({
                type: verb,
                url: targetUrl,
                data: dataToSend,
                headers:
                {
                    'X-Requested-With': 'XMLHttpRequest'
                },
                success: function (result) {
                    console.log(result);
                    HandleAjaxResult(result);
                    LoadCallback();
                    enableElement(target);
                    $target.removeClass('auto-loader');
                },
                error: function (result) {
                    $.notify({ message: "Une erreur s'est produite." }, { type: 'fatal' });
                    $target.removeClass('auto-loader');
                },
            });
            return false;
        });
    }
}

function disableElement(element) {
    $(element).addClass("disabled");
    $(element).attr("disabled", "disabled");
}

function enableElement(element) {
    $(element).removeClass("disabled");
    $(element).removeAttr("disabled");
}

// When getting a reponse from a ajax call, decides what to do.
function HandleAjaxResult(result) {
    console.debug(result);

    // Multiple results, we call HandleAjaxResult for every item.
    if (Array.isArray(result)) {
        for (var i = 0; i < result.length; i++) {
            HandleAjaxResult(result[i].value);
        }
    }

    if (result.errorMessage != undefined) {
        $.notify({ message: result.errorMessage }, { type: 'danger' });
    }

    if (result.warnMessage != undefined) {
        $.notify({ message: result.warnMessage }, { type: 'warning' });
    }

    if (result.successMessage != undefined) {
        $.notify({ message: result.successMessage }, { type: 'success' });
    }

    if (result.fatalMessage != undefined) {
        $.notify({ message: result.fatalMessage }, { type: 'fatal' });
    }

    if (result.openUrl != undefined) {
        $('#siteloader').show();
        $("#siteloader").attr("src", result.openUrl);
    }

    if (result.removeHtml != undefined) {
        let selector = $(result.removeHtml.selector);
        $(selector).remove();
    }

    if (result.replaceHtml != undefined) {
        let selector = $(result.replaceHtml.selector);
        if (selector.length > 0) {
            let url = result.replaceHtml.action;

            let loaderClass = "loader";
            if (result.replaceHtml.loaderClass != undefined && result.replaceHtml.loaderClass != null && result.replaceHtml.loaderClass != "") {
                loaderClass = result.replaceHtml.loaderClass;
            }

            // Display a loader.
            //selector.html("<div class='" + loaderClass + "'></div>");

            $.ajax({
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                },
                type: "GET",
                url: url,
                success: function (result2) {
                    selector.html(result2).hide().fadeIn(1000);
                    LoadCallback(selector);

                    // If we display a full page we add it to the user's page history.
                    if (result.replaceHtml.selector == "#page") {
                        history.pushState({ url: url }, "replaceHtml", url);
                        document.body.scrollTop = document.documentElement.scrollTop = 0;
                    }
                },
                error: function (result) {
                    $.notify({ message: "Une erreur s'est produite, redirection vers la page d'accueil" }, { type: 'fatal' });
                }
            });
        }
    }

    if (result.appendHtml != undefined) {
        let selector = $(result.appendHtml.selector);
        if (selector.length > 0) {
            let url = result.appendHtml.action;
            $.get(url, function (result2) {
                $(result2).hide().appendTo(selector).fadeIn(1000);
                LoadCallback(selector);
            });
        }
    }

    if (result.openNewWindow != undefined) {
        setTimeout(function () {
            window.open(result.openNewWindow.url, '_blank');
        }, 1000);
    }

    if (result.hideHtml != undefined) {
        let selector = $(result.hideHtml.selector);
        selector.hide();
    }

    if (result.loadUrl != undefined) {
        location.href = result.loadUrl;
    }
}