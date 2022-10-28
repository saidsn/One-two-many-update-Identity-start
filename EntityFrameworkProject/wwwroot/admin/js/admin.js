
$(function () {


    $(document).on("click", ".delete-social", function () {


        let socialId = parseInt($(this).attr("data-id"))

        let data = { id: socialId }

        let bodyElem = $("#table-body");

      

        $.ajax({
            url: "/adminarea/social/delete",
            type: "POST",
            data: data,
            contentType: "application/x-www-form-urlencoded",
            success: function (res) {
                debugger

                let htmlData = "";
                for (var i = 0; i < res.length; i++) {
                    htmlData += `<tr>
                    <td>
                        ${res[i].name}
                    </td>
                    <td>
                         ${res[i].url}
                    </td>

                    <td style="text-align: center;">
                        <a href="/adminarea/social/detail?id=${res[i].id}"  class="btn btn-info"><i class="mdi mdi-information mx-0"></i></a>
                        <a href="/adminarea/social/edit?id=${res[i].id}"  class="btn btn-primary"><i class="mdi mdi-table-edit"></i></a>
                        <button class="btn btn-danger delete-social" data-id="${res[i].id}"><i class="mdi mdi-delete-forever"></i></button>
                    </td>
                </tr>`
                }
               

                bodyElem.html(htmlData);
            }
        })

    });




})