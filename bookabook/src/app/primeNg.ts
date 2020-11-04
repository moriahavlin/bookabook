import { NgModule } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { FileUploadModule } from 'primeng/fileupload';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationService } from 'primeng/api'
import {RatingModule} from 'primeng/rating';
import {ChartModule} from 'primeng/chart';

@NgModule({

    imports: [
        ButtonModule,
        FileUploadModule,
        RatingModule,
        ChartModule
    ],
    exports: [

        ButtonModule,
        FileUploadModule,
        RatingModule,
        ChartModule
    ],

})
export class PrimeNgModule { }
