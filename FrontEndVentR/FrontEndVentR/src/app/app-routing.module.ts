import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//components
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ShopcartComponent } from './pages/shopcart/shopcart.component';
import { BuyComponent } from './pages/buy/buy.component';
import { SearchComponent } from './pages/search/search.component';
import { ProductComponent } from './pages/product/product.component';

//Guards
import { AuthGuard } from './guards/auth.guard';
import { NotpagefoundComponent } from './shared/notpagefound/notpagefound.component';
import { PagesComponent } from './pages/pages.component';
import { SignUpComponent } from './shared/sign-up/sign-up.component';
import { SignInComponent } from './shared/sign-in/sign-in.component';



const routes: Routes = [
  {
    path: '',
    component: PagesComponent,
    children: [
      { path: 'home', component: HomeComponent},
      { path: 'profile', component: ProfileComponent, canActivate:[ AuthGuard]},
      { path: 'shopcart', component: ShopcartComponent, canActivate:[ AuthGuard]},
      { path: 'buy', component: BuyComponent, canActivate:[ AuthGuard]},
      { path: 'product/:id', component: ProductComponent},
      { path: 'results/:text', component: SearchComponent},
      { path: '', redirectTo: '/home', pathMatch: 'full' }
    ]
   },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: '**', component: NotpagefoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
