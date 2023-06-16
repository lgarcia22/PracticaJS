$.holdReady(true);
$.when(
    $.getJSON("/HIS/Scripts/cldr/main/es/ca-gregorian.json"),
    $.getJSON("/HIS/Scripts/cldr/main/es/numbers.json"),
    $.getJSON("/HIS/Scripts/cldr/main/es/currencies.json"),
    $.getJSON("/HIS/Scripts/cldr/supplemental/likelySubtags.json"),
    $.getJSON("/HIS/Scripts/cldr/supplemental/timeData.json"),
    $.getJSON("/HIS/Scripts/cldr/supplemental/weekData.json"),
    $.getJSON("/HIS/Scripts/cldr/supplemental/currencyData.json"),
    $.getJSON("/HIS/Scripts/cldr/supplemental/numberingSystems.json")
).then(function () {
    return [].slice.apply(arguments, [0]).map(function (result) {
        return result[0];
    });
}).then(
    Globalize.load
).then(function () {
    Globalize.locale('es');
    $.holdReady(false);
}).catch(console.log);