import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClaimsComponent } from './claims/claims.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: 'claims', component: ClaimsComponent , canActivate: [AuthGuard]},
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
