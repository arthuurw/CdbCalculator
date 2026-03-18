import { Routes } from '@angular/router';
import { HomeComponent } from './features/cdb-simulation/pages/home/home.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: '**',
        redirectTo: ''
    }
];