
//
// Include all the unpacked/minified versions of the javascript files we use
//
var incldues = [
    'daemachTools.js',
    'grid.base.js',
    'grid.custom.js',
    'grid.formedit.js',
    'grid.inlinedit.js',
    'grid.postext.js',
    'grid.subgrid.js',
    'grid.treegrid.js',
    'jqDnR.js',
    'jqModal.js',
    'jquery.date_input.js',
    'jquery.jqGrid.js',
    'jquery.metadata.js',
    'jquery.tableFilter.aggregator.js',
    'jquery.tableFilter.columnStyle.js',
    'jquery.tableFilter.js',
    'jquery.tablesorter.js',
    'jquery.tablesorter.min.js',
    'Template.js'
];
var head = $("head");
$(includes).each(function (i, item) {
    $(head).append("<script src='../Resources/i/" + item + "' type='text/javascript'></scr" + "ipt>");
});