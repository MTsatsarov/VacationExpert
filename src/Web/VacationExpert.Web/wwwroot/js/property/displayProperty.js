document.getElementById("show-review-btn").addEventListener("click", showHideReview)
var reviewBox = document.getElementsByClassName('create-review-box')[0];

function showHideReview() {

    if (reviewBox.style.display == 'none' || reviewBox.style.display=="") {
        reviewBox.style.display = 'flex';
    }
    else {
        reviewBox.style.display = 'none';
    }
}