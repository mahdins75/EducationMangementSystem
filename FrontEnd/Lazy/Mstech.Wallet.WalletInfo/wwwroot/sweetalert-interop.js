window.sweetAlertServices = {
    showAlert: function (title, text, icon) {
        return Swal.fire(title, text, icon);  // Ensure you use Swal, not swal
    },
    showConfirmation: function (title, text) {
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
        return result.isConfirmed;  // This will be true or false
    }
};