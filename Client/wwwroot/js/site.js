// Write your JavaScript code.
$(document).ready(function () {
    if (document.getElementById("ApiUrl") !== null) {
        if (localStorage.getItem("ApiUrl") === null || localStorage.getItem("ApiUrl" !== document.getElementById("ApiUrl").innerHTML)) {
            localStorage.setItem("ApiUrl", document.getElementById("ApiUrl").innerHTML);
        }
        timerJob();
    }
    console.log('documentReady');
});

$(".uppercase").keyup(function () {
    var text = $(this).val();
    $(this).val(text.toUpperCase());
});

function clearErrors() {
    $(".validation-summary-errors").empty();
};

function timerJob() {
    const tenSeconds = 10000;
    const oneSecond = 1000;
    $.ajax({
        url: localStorage.getItem("ApiUrl") + "api/book",
        type: "GET",
        dataType: "json",
        success: function (books) {
            if (books.length === 0) {
                setTimeout(timerJob, oneSecond);
                console.log("No books found!");
                return;
            }
            const selectedItem = Math.floor(Math.random() * books.length);
            let selectedBook = books[selectedItem];
            if (selectedBook.disabled === true) {
                console.log(selectedBook.Title + " is blocked for uppdating of InShelf/Offline!");
                return;
            }
            selectedBook.InShelf = !selectedBook.InShelf;
            $.ajax({
                url: localStorage.getItem("ApiUrl") + "api/book/" + selectedBook.id,
                contentType: "application/json",
                type: "PUT",
                data: JSON.stringify(selectedBook),
                dataType: "json"
            });

            const selector = `#${selectedBook.id} td:eq(2)`;
            const selector2 = `#${selectedBook.id + "_2"} td:eq(3)`;
            const selector3 = `#${selectedBook.id + "_3"}`;
            if (selectedBook.InShelf === true) {
                $(selector).text("InShelf");
                $(selector).removeClass("alert-danger");
                $(selector2).text("InShelf");
                $(selector2).removeClass("alert-danger");
                $(selector3).text("InShelf");
                $(selector3).removeClass("alert-danger");
                console.log(selectedBook.Title + " is InShelf!");
            }
            else {
                $(selector).text("Offline");
                $(selector).addClass("alert-danger");
                $(selector2).text("Offline");
                $(selector2).addClass("alert-danger");
                $(selector3).text("Offline");
                $(selector3).addClass("alert-danger");
                console.log(selectedBook.Title + " is Offline!");
            }
            if (document.getElementById("All") !== null) {
                doFiltering();
            }
        }
    });
    setTimeout(timerJob, tenSeconds);
}

function doFiltering() {
    let selection = 0;
    let radiobtn = document.getElementById("All");
    if (radiobtn.checked === false) {
        radiobtn = document.getElementById("InShelf");
        if (radiobtn.checked === true) {
            selection = 1;
        }
        else {
            selection = 2;
        }
    }

    var table = $('#books > tbody');
    $('tr', table).each(function () {
        $(this).removeClass("hidden");
        let td = $('td:eq(2)', $(this)).html();
        if (td !== undefined) {
            td = td.trim();
        }
        if (td === "Offline" && selection === 1) {
            $(this).addClass("hidden");  //Show only InShelf
        }
        if (td === "InShelf" && selection === 2) {
            $(this).addClass("hidden"); //Show only Offline
        }
    });
};

function showModals() {
    window.open("./html/BookOverview.html", "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=50,width=500,height=400");
}
