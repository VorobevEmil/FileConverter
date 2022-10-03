function sendHtmlFile() {
    const file = document.getElementById('uploadFile').files[0];
    if (file.type != 'text/html') {
        alert("error, this is not an html file");
        return;
    }
    var formData = new FormData();
    formData.append('file', file);
    var progress = document.getElementById('progress');
    var progressInfo = document.getElementById('progressInfo');
    var convertButton = document.getElementById('convertButton');
    progressInfo.innerHTML = '';
    progressInfo.innerHTML =
        '<div class="sk-chase">' +
        '<div class="sk-chase-dot"></div>' +
        '<div class="sk-chase-dot"></div>' +
        '<div class="sk-chase-dot"></div>' +
        '<div class="sk-chase-dot"></div>' +
        '<div class="sk-chase-dot"></div>' +
        '<div class="sk-chase-dot"></div>' +
        '</div>';
    convertButton.disabled = true;

    // отослать
    var xhr = new XMLHttpRequest();

    xhr.upload.onprogress = function (event) {
        if (event.loaded != event.total)
            progress.value = 'Загружено на сервер ' + event.loaded + ' байт из ' + event.total;
        else
            progress.value = 'Идёт обработка, подождите...';
    }
    xhr.onload = function (e) {
        progressInfo.innerHTML = '';
        if (this.status == 200) {
            progress.value = 'Конвертирование завершенно!';
            var blob = new Blob([this.response], { type: 'application/pdf' }),
                url = URL.createObjectURL(blob),
                linkDownloadFile = document.createElement('a');

            linkDownloadFile.innerText = "Download PDF File";
            linkDownloadFile.setAttribute('href', url);
            linkDownloadFile.setAttribute('class', 'btn btn-primary');
            linkDownloadFile.setAttribute('style', 'border: 1px solid; border-radius: 10px; width: 300px; height: 40px;');

            progressInfo.append(linkDownloadFile);
        }
        else {
            progress.value = xhr.responseText;
        }

        convertButton.disabled = false;
    };
    xhr.upload.onerror = function () {
        progressInfo.innerHTML = '';
        progress.value = 'Произошла ошибка при загрузке данных на сервер!';
    }

    xhr.open("POST", "/api/FileConvert/HtmlToPdf", true);
    xhr.responseType = 'blob';

    xhr.send(formData);
}