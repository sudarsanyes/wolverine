import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card } from '../Contracts/Contracts';
import { ClipboardService } from 'ngx-clipboard';
import { Router, ActivatedRoute } from '@angular/router';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'sort',
  templateUrl: './sort.component.html'
})
export class SortComponent {

  ActiveProject: Project;
  Id: string;
  IsSaving: boolean;
  IsSavingSuccessful: boolean;
  IsDirty: boolean;
  Groups = [];

  constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute, private clipboardService: ClipboardService) {
    // Reserved. 
  }

  ngOnInit() {
    this.Id = this.route.snapshot.paramMap.get('id');
    this.projectService.load(this.Id).subscribe((project: Project) => {
      this.ActiveProject = project;
      for (let group of this.ActiveProject.groups) {
        this.Groups.push(group.id);
      }
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      console.log("same container");
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      console.log("different container");
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }
}