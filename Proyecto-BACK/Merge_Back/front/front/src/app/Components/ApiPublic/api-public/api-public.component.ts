import { Component, OnInit, ViewChild } from '@angular/core';
import { ApiPublicService, Attraction } from '../../../Services/ApiPublic/api-public.service';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-api-public',
  standalone: true,
  imports: [CommonModule, MatSnackBarModule, MatButtonModule, MatTableModule, MatPaginatorModule, MatPaginator, MatCheckboxModule, RouterLink],
  templateUrl: './api-public.component.html',
  styleUrls: ['./api-public.component.css']
})
export class ApiPublicComponent implements OnInit {
  public attractions: Attraction[] = [];
  public selectedAttractions: Attraction[] = [];
  public saving = false;

  displayedColumns: string[] = ['select', 'name', 'description'];
  dataSource = new MatTableDataSource<Attraction>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private attractionService: ApiPublicService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.loadAttractions();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  loadAttractions() {
    this.attractionService.getPublicAttractions().subscribe(data => {
      this.attractions = data;
      this.dataSource.data = data;
    });
  }

  toggleSelection(attraction: Attraction) {
    const index = this.selectedAttractions.findIndex(a => a.name === attraction.name);
    if (index >= 0) {
      this.selectedAttractions.splice(index, 1);
    } else {
      this.selectedAttractions.push(attraction);
    }
  }

  saveSelected() {
    if (this.selectedAttractions.length === 0) return;

    this.saving = true;

    this.attractionService.saveAttractions(this.selectedAttractions).subscribe({
      next: (response) => {
        this.showSuccessNotification(response.saved);
        this.selectedAttractions = [];
        this.saving = false;
      },
      error: (err) => {
        console.error('Error al guardar:', err);
        this.showErrorNotification();
        this.saving = false;
      }
    });
  }

  private showSuccessNotification(count: number) {
    this.snackBar.open(
      `¡Éxito! ${count} atracción(es) guardada(s) correctamente.`,
      'Cerrar',
      {
        duration: 5000,
        panelClass: ['success-snackbar']
      }
    );
  }

  private showErrorNotification() {
    this.snackBar.open(
      'Error al guardar las atracciones. Por favor, inténtelo de nuevo.',
      'Cerrar',
      {
        duration: 5000,
        panelClass: ['error-snackbar']
      }
    );
  }

  allSelected(): boolean {
    return this.selectedAttractions.length === this.dataSource.data.length;
  }

  someSelected(): boolean {
    return this.selectedAttractions.length > 0 && !this.allSelected();
  }

  toggleAllRows(event: any) {
    if (event.checked) {
      this.selectedAttractions = [...this.dataSource.data];
    } else {
      this.selectedAttractions = [];
    }
  }
}