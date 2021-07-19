import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'jsapp';

   constructor(
    protected readonly router: Router,
     private readonly keycloakService: KeycloakService     
     ) { }

   onLogout(){
     this.keycloakService.logout('http://' + location.host);     
   }
}
