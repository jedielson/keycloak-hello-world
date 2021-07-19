import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiServiceService, Claim } from './api-service.service';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-claims',
  templateUrl: './claims.component.html',
  styleUrls: ['./claims.component.less']
})
export class ClaimsComponent implements OnInit {

  claims: Claim[] = [];
  displayedColumns: string[] = ['type', 'value'];

  
  table:MatTableDataSource<Claim> = new MatTableDataSource<Claim>([]);

  constructor(    
    private readonly httpService: ApiServiceService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.getAllClaims();
  }

  getAllClaims() {    
    this.httpService.getClaims().subscribe(
      response => {
        this.claims = response;
        this.table.data = response as Claim[];
      },

      error => {
        this.handleError(error.error)
      }
    )
  }

  private handleError(error: any) {
    this.displayError(error.code + ' ' + error.reason + ". " + error.message)
  }

  private displayError(message: string) {
    this.snackBar.open(message, 'Close', { duration: 5000 })
  }

}
