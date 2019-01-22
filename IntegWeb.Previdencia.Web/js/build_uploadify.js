

var isOpera = (!!window.opr && !!opr.addons) || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
// Firefox 1.0+
var isFirefox = typeof InstallTrigger !== 'undefined';
// At least Safari 3+: "[object HTMLElementConstructor]"
var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
// Internet Explorer 6-11
var isIE = /*@cc_on!@*/false || !!document.documentMode;
// Edge 20+
var isEdge = !isIE && !!window.StyleMedia;
// Chrome 1+
var isChrome = !!window.chrome && !!window.chrome.webstore;
// Blink engine detection
var isBlink = (isChrome || isOpera) && !!window.CSS;

function sendFile(file) {

    var formData = new FormData();
    formData.append('file', $('#ContentPlaceHolder1_FileUploadControl')[0].files[0]);
    $.ajax({
        type: 'post',
        url: 'ArquivoPatrocinadora.ashx',
        data: formData,
        success: function (status) {
            if (status != 'error') {
                var my_path = "MediaUploader/" + status;
                $("#myUploadedImg").attr("src", my_path);
            }
        },
        processData: false,
        contentType: false,
        error: function () {
            alert("Whoops something went wrong!");
        }
    });
}

function build_uploadify() {

    //$('#ContentPlaceHolder1_FileUploadControl').uploadify({
    $("input[id*='FileUploadControl']").uploadify({
        'swf': 'js/uploadify/uploadify.swf',
        'uploader': 'UploadFile.ashx',
        'fileDataName': 'file',
        'buttonText': 'Enviar Arquivos',
        'multi': true,
        'sizeLimit': 1048576,
        'simUploadLimit': 2,
        'auto': true,
        'removeCompleted': false,
        'force_replace': true,
        'width': 125,
        'height': 13,
        'buttonImage': 'img/upload.png',
        'wmode': 'transparent',
        'queueID': true, //Esconde lista de Uploads
        'onUploadError': function (file, errorCode, errorMsg, errorString) {
            alert('The file ' + file.name + ' could not be uploaded: ' + errorString);
        },
        'onQueueComplete': function (queueData) {
            //alert(queueData.uploadsSuccessful);
            //postbackButtonClick();
            $('#ContentPlaceHolder1_btnProcessar').click();
        },
        'onSelect': function (file) {
        },
        'onCheck': function (file, exists) {
            if (exists) {
                alert('upload failed because the file is a duplicate');
            }
        }
    });

    $('#SWFUpload_0').attr("width", 151).attr("height", 29);

}

////} else if (isChrome) {
//    //var _URL = window.URL || window.webkitURL;
//    $("#ContentPlaceHolder1_FileUploadControl").on('change', function () {

//        if (isChrome) {

//            var file, img;
//            if ((file = this.files[0])) {
//                //img = new File();
//                //img.onload = function () {
//                debugger
//                sendFile(file);
//                //};
//                //img.onerror = function (e, data) {
//                //    debugger
//                //    alert("Not a valid file:" + file.type + " - ex: " + e);
//                //};
//                //img.src = _URL.createObjectURL(file);
//            }
//        } else if (isIE) {
//            sendFileIE();
//        }
//    });
//}       