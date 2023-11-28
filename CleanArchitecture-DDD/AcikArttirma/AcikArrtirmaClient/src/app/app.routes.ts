import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "acik-arttirma/:value",
        loadComponent: ()=> 
                    import("./components/acik-arttirma/acik-arttirma.component")
    }
];
