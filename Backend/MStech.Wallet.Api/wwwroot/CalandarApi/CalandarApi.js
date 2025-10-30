var events = [];
var timePeriod = [];
function GetAllEvents(filter, pageLoad) {
  return new Promise(function (resolve, reject) {
    var url = "/Admin/Calendar/GetAll";
    if (filter) {
      url = url + "?filter=" + filter;
    }
    $.ajax({
      url: url,
      type: "GET",
      data: filter,
      dataType: "json",
      contentType: "application/json; charset=utf-8",
      success: function (data) {
        events = data.entity;
      },
    }).done(function () {
      if (!pageLoad) {
        reloadcalandar();
      }
      resolve();
    });
  });
}
function addEvent(event) {
  return $.ajax({
    url: "/Admin/Calendar/Create",
    type: "POST",
    data: event,
    dataType: "json",
    success: function (data) {
      if (data.isSuccess) {
        toastr.success("ثبت رویداد با موفقیت انجام شد", "عملیات موفق", {
          rtl: true,
          positionClass: "toast-bottom-right",
          containerId: "toast-bottom-right",
        });
      } else {
        toastr.error(" ثبت رویداد با خطا مواجه شد.", "اخطار!", {
          rtl: true,
          positionClass: "toast-bottom-right",
          containerId: "toast-bottom-right",
        });
      }
    },
  }).done(function () {
    $("#EventModal").modal("hide");
    GetAllEvents(null, false);
    resetForm();
  });
}
function updateEvent(event) {
  return $.ajax({
    url: "/Admin/Calendar/Update",
    type: "POST",
    data: event,
    dataType: "json",
    success: function (data) {
      if (data.isSuccess) {
        toastr.success("ویرایش رویداد با موفقیت انجام شد", "عملیات موفق", {
          rtl: true,
          positionClass: "toast-bottom-right",
          containerId: "toast-bottom-right",
        });
      } else {
        toastr.error(" ویرایش رویداد با خطا مواجه شد.", "اخطار!", {
          rtl: true,
          positionClass: "toast-bottom-right",
          containerId: "toast-bottom-right",
        });
      }
    },
  }).done(function () {
    $('input[name="Id"]').val(0);
    $("#EventModal").modal("hide");
    GetAllEvents(null, false);
    resetForm();
  });
}
function deleteEvent(id) {
  return $.ajax({
    url: "/Admin/Calendar/Delete",
    type: "POST",
    data: { id: id },
    dataType: "json",
    success: function (data) {
      if (data.isSuccess) {
        GetAllEvents(null, false);
      }
    },
  }).done(function () {
    GetAllEvents(null, false);
  });
}
