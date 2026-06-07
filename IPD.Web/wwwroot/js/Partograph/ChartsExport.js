

$('#btnPrintPartograph').click(function () {

    makePdf();

});



window.html2canvas = html2canvas;
window.jsPDF = window.jspdf.jsPDF;
function makePdf() {
    var form = $('#printArea');
    html2canvas(form[0], {
        allowTent: true,
        useCORS: true,
        scale: 0.7
    }).then((canvas) => {
        var img = canvas.toDataURL('image/PNG');
        var doc = new jsPDF({
            orientation: 'p',
            unit: 'in',
            format: [8.27, 17.2],
            putOnlyUsedFonts: true
        });
        doc.setFont('Arial');
        doc.getFontSize(11);
        doc.addImage(img, 'PNG', 0, 0);
        doc.save('partograph.pdf');
    });
}


