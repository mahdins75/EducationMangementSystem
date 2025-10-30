function getAllApplicationFormComment() {
    var applicationFomrId = $("#selectedApplicationFormId").val();
    $.ajax({
        url: '/Admin/ApplicationFormComment/GetAll?applicationFormId=',
        type: 'get',
        success: function (data) {
            var htmlContent = `<div class="card">`;
            if (data.isSuccessed) {
                data.entity.forEach(function (index, item) {
                    htmlData +=` <div class="card-header">
                                                    <div class="avatar m-0" style="margin-left: 8px !important;">
                                                        <img src="/assets/images/avatar.png" alt="avtar img holder"
                                                             height="32" width="32">
                                                    </div>
                                                    امین بقائی
                                                    <span class="light ml-2">${item.persianCreateDate}</span>

                                                    <div class="heading-elements mt-1">
                                                        <a href="#" class="dark"><i class="bx bx-lock-open-alt"></i></a>
                                                        <a href="#" onclick="editApplicationComment(${item.id})"  class="dark"><i class="bx bx-edit-alt"></i></a>
                                                        <a href="#"  onclick="deleteApplicationComment(${item.id})" class="dark"><i class="bx bx-trash"></i></a>
                                                    </div>
                                                     <div class="card-body">
                                                    <p>
                                                        ${item.Description}
                                                    </p>
                                                </div>
                                 </div>`;
                })
                htmlContent +=`</div>`;
                $("#applicationFormCommentArea").html(htmlContent)

            }
            else {
                var htmlContent = "";
                htmlContent += `
                     <div class="alert bg-rgba-primary alert-dismissible mb-2" role="alert">
                          <div class="d-flex align-items-center">
                              <span>
                                  تاکنون یادداشتی برای این متقاضی ثبت نشده است!
                              </span>
                          </div>
                     </div>
                    `;
                $("#applicationFormCommentArea").html(htmlContent)
            }
        }
    })
}

function editApplicationComment(id) {
    //make commentEditable 
    $.ajax({
        url:'/'
    })

}
function deletepplicationComment(id) {

}