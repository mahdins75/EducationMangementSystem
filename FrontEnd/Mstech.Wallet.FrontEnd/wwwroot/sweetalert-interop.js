window.sweetAlertServices = {
    showAlert: function (title, text, icon) {
        return Swal.fire(title, text, icon); // Ensure you use Swal, not swal
    },
    showConfirmation: async function (title, text) { // Added 'async' keyword here
        const result = await Swal.fire({
            title: title,
            text: text,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'No'
        });
        debugger;
        // Return true if user confirmed, false if they canceled
        return result.isConfirmed; // This will be true or false
    }
};


window.showToast = (message, type) => {
    // Ensure SweetAlert2 is loaded before this function is called
    Swal.fire({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        icon: type, // 'success', 'error', 'info', 'warning', 'question'
        title: message
    });
};
