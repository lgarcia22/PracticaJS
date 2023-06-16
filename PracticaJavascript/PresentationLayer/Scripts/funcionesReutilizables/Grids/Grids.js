async function ExportarPdf(idDxGrid, title) {
    try {
        idDxGrid = getParsedId(idDxGrid);
        window.jsPDF = window.jspdf.jsPDF;
        applyPlugin(window.jsPDF);
        var options = {
            orientation: 'l',
            format: 'a4',
            unit: 'mm',
        };
        var doc = new jsPDF(options);
        doc.setTextColor('#FFFFFF');
        doc.autoTable({
            headStyles: { textColor: 21, horizontalPageBreak: true },
            styles: { textColor: 21, horizontalPageBreak: true },
            columnStyles: { 0: { halign: 'center' } },
            body: [
                [{ content: title, colSpan: 2, rowSpan: 20, styles: { halign: 'center' }, lineColor: 11 }],
            ],
        });
        var dataGrid = $(idDxGrid).dxDataGrid("instance");
        await DevExpress.pdfExporter.exportDataGrid({
            jsPDFDocument: doc,
            component: dataGrid,
            customizeCell: function (options) {
                test = options;
                const { gridCell, pdfCell } = options;
                if (gridCell.rowType === 'data') {
                    pdfCell.styles = {
                        font: 'times',
                        fontSize: 14,
                        overflow: 'linebreak'
                    }
                }
            }
        });

        doc.save(title + ".pdf");
    } catch (e) {
        console.log("ExportToPdf: ", e);
    }

}

async function ExportarCsv(idDxGrid, title) {
    try {
        idDxGrid = getParsedId(idDxGrid);

        var dataGrid = $(idDxGrid).dxDataGrid("instance");
        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet(title);

        await DevExpress.excelExporter.exportDataGrid({
            component: dataGrid,
            worksheet: worksheet,
        });

        var buffer = await workbook.csv.writeBuffer();

        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), title + '.csv');
    } catch (e) {
        console.log("ExportToCsv: ", e);
    }
}

async function ExportarXls(idDxGrid, title) {
    try {
        idDxGrid = getParsedId(idDxGrid);

        var dataGrid = $(idDxGrid).dxDataGrid("instance");

        var workbook = new ExcelJS.Workbook();
        var worksheet = workbook.addWorksheet(title);

        await DevExpress.excelExporter.exportDataGrid({
            component: dataGrid,
            worksheet: worksheet,
            autoFilterEnabled: true
        });

        var buffer = await workbook.xlsx.writeBuffer();
        saveAs(new Blob([buffer], { type: 'application/octet-stream' }), title + '.xlsx');
    } catch (e) {
        console.log("ExportToXls: ", e);
    }
}

function RefrescarGrid(idDxGrid) {
    try {
        idDxGrid = getParsedId(idDxGrid);

        if (idDxGrid[0] !== "#") {
            idDxGrid = "#" + idDxGrid;
        };

        $(idDxGrid).dxDataGrid("instance").refresh();

    } catch (e) {
        console.log("RefreshGridData: ", e);
    }
}