(function ($) {
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

$(function () {
    LoadCallback();
    $('[type="checkbox"]').not("[readonly='readonly']").bootstrapSwitch();
});

function LoadCallback(selector) {
    $("[ns-loader-url]").each(function (e) {
        let thisdiv = $(this);
        let url = $(this).attr("ns-loader-url");
        $(this).removeAttr("ns-loader-url");

        $.get(url, function (partial) {
            thisdiv.replaceWith(partial);
            LoadCallback();
        });
    });

    $("[ns-action-type=simpleGetAction]").off('click').click(function (e) {
        let target = e.target;

        //we could have clicked the icon <i> element
        if ($(e.target).is("i"))
            target = $(e.target).parent().first()[0];

        let targetUrl = target.getAttribute("ns-action-url");

        console.log('clicked' + targetUrl);
        $.ajax({
            type: "GET",
            url: targetUrl,
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (result) {
                console.log(result);
                HandleAjaxResult(result);
                LoadCallback();
            }
        });
        return false;
    });

    $("[ns-action-type=simplePostAction]").off('click').click(function (e) {
        let target = e.target;

        //we could have clicked the icon <i> element
        if ($(e.target).is("i"))
            target = $(e.target).parent().first()[0];

        let targetUrl = target.getAttribute("ns-action-url");

        console.log('clicked' + targetUrl);
        $.ajax({
            type: "POST",
            url: targetUrl,
            data: $(e.target).closest("form").serializeFormJSON(),
            success: function (result) {
                console.log(result);
                HandleAjaxResult(result);
                LoadCallback();
            }
        });
        return false;
    });
}


function HandleAjaxResult(result) {
    console.debug(result);

    //multiple results, we call HandleAjaxResult for every item.
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

    if (result.OpenUrl != undefined) {
        $('#siteloader').show();
        $("#siteloader").attr("src", result.OpenUrl);
    }

    if (result.replaceHtml != undefined) {
        let selector = $(result.replaceHtml.selector);
        if (selector.length > 0) {
            var url = result.replaceHtml.action;
            $.get(url, function (result2) {
                selector.html(result2).hide().fadeIn(1000);
                LoadCallback(selector);

                //if we display a full page we add it to the user's page history
                if (selector == "#page")
                    history.pushState(null, "replaceHtml", url);
            });
        }
    }

    if (result.appendHtml != undefined) {
        let selector = $(result.appendHtml.selector);
        if (selector.length > 0) {
            var url = result.appendHtml.action;
            $.get(url, function (result2) {
                $(result2).hide().appendTo(selector).fadeIn(1000);
                LoadCallback(selector);
            });
        }
    }

    if (result.openNewWindow != undefined) {
        var url = result.openNewWindow.url;
        window.open(url, '_blank');
    }

    if (result.hideHtml != undefined) {
        let selector = $(result.hideHtml.selector);
        selector.hide();
    }

    if (result.loadUrl != undefined) {
        location.href = result.loadUrl;
    }
}