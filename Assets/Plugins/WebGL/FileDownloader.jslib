mergeInto(LibraryManager.library, {
    DownloadFile: function (filenamePtr, contentPtr) {
        var filename = UTF8ToString(filenamePtr);
        var content = UTF8ToString(contentPtr);

        var blob = new Blob([content], { type: "text/csv;charset=utf-8;" });
        var link = document.createElement("a");

        link.href = URL.createObjectURL(blob);
        link.download = filename;
        link.click();
    }
});