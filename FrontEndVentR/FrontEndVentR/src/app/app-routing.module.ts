import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//components
import { HomeComponent } from './pages/home/home.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { ShopcartComponent } from './pages/shopcart/shopcart.component';
import { BuyComponent } from './pages/buy/buy.component';
import { SearchComponent } from './pages/search/search.component';
import { ProductComponent } from './pages/product/product.component';

//Guards
import { AuthGuard } from './guards/auth.guard';



const routes: Routes = [
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'home', component: HomeComponent},
  { path: 'profile', component: ProfileComponent, canActivate:[ AuthGuard]},
  { path: 'shopcart', component: ShopcartComponent, canActivate:[ AuthGuard]},
  { path: 'buy', component: BuyComponent, canActivate:[ AuthGuard]},
  { path: 'product/:id', component: ProductComponent},
  { path: 'results/:text', component: SearchComponent},  
  { path: '**', pathMatch: 'full', redirectTo: 'home' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
