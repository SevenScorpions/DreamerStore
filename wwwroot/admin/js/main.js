(function ($) {

    "use strict";

    //Upload Image
	$(document).ready(function () {
        var names = [];
        $('body').on('change', '.upload__inputfile', function (event) {
            var getAttr = $(this).attr('click-type');
            var files = event.target.files;
            if (getAttr == 'type1') {
                $('.upload__img-wrap').html('');
                $('.upload__img-wrap').html(
                    `<div class="upload__img-box">
                    <label class="plus__btn">                   
                        <div style="background-image: url('~/assets/images/plus.png')" class="img-bg"></div>
                        <input type="file" click-type="type2" multiple="" class="upload__inputfile">                    
                    </label>
                </div>`,
                );
                $('#hint_brand').modal('show');

                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    names.push($(this).get(0).files[i].name);
                    var picReader = new FileReader();
                    picReader.fileName = file.name;
                    picReader.addEventListener('load', function (event) {
                        var picFile = event.target;

                        var html = `<div class="upload__img-box">
                            <div style="background-image: url('${picFile.result}')" class="img-bg">                              
                                <a href="javascript:void(0);" data-id="${picFile.fileName}" class="upload__img-close"></a>                              
                            </div>
                        </div>`;

                        $('.upload__img-wrap').append(html);
                    });
                    picReader.readAsDataURL(file);
                }
                console.log(names);
            } else if (getAttr == 'type2') {
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];
                    names.push($(this).get(0).files[i].name);
                    var picReader = new FileReader();
                    picReader.fileName = file.name;
                    picReader.addEventListener('load', function (event) {
                        var picFile = event.target;

                        var html = `<div class="upload__img-box">
                            <div style="background-image: url('${picFile.result}')" class="img-bg">                               
                                <a href="javascript:void(0);" data-id="${picFile.fileName}" class="upload__img-close"></a>                              
                            </div>
                        </div>`;

                        $('.upload__img-wrap').append(html);
                    });
                    picReader.readAsDataURL(file);
                }
                // return array of file name
                console.log(names);
            }
        });

        $('body').on('click', '.upload__img-close', function () {
            $(this).parent().parent().remove();
            var removeItem = $(this).attr('data-id');
            var yet = names.indexOf(removeItem);

            if (yet != -1) {
                names.splice(yet, 1);
            }
            // return array of file name
            console.log(names);
        });
        $('#hint_brand').on('hidden.bs.modal', function (e) {
            names = [];
            z = 0;
        });
	});

})(window.jQuery);
