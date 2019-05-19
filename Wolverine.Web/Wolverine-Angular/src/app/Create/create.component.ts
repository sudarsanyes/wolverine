import { Component } from '@angular/core';
import { ProjectService } from '../Services/project.service';
import { Project, Group, Card, Guid } from '../Contracts/Contracts';
import { Router, ActivatedRoute } from '@angular/router';
import { ClipboardService } from 'ngx-clipboard'

@Component({
    selector: 'create',
    templateUrl: './create.component.html'
})
export class CreateComponent {

    ActiveProject: Project;
    ID: string;
    NewCard: Card;
    NewGroup: Group;
    IsSaving: boolean;
    IsSavingSuccessful: boolean;
    IsDirty: boolean;

    constructor(private projectService: ProjectService, private router: Router, private route: ActivatedRoute, private clipboardService: ClipboardService) {
        this.NewCard = Card.getDefault();
        this.NewGroup = Group.getDefault();
    }

    ngOnInit() {
        this.ID = this.route.snapshot.paramMap.get('name');
        this.projectService.load(this.ID).subscribe((project: Project) => {
            this.ActiveProject = project;
        });
    }

    onEditCard(card: Card) {
        this.IsDirty = true;
    }

    onDeleteCard(card: Card) {
        var index = 0;
        for (let c of this.ActiveProject.unsortedGroup.cards) {
            if (c.id == card.id) {
                this.ActiveProject.unsortedGroup.cards.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onAddCard() {
        this.ActiveProject.unsortedGroup.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
        this.IsDirty = true;
    }

    onEditGroup(group: Group) {
        this.IsDirty = true;
    }

    onDeleteGroup(group: Group) {
        var index = 0;
        for (let g of this.ActiveProject.defaultGroups) {
            if (g.id == group.id) {
                this.ActiveProject.defaultGroups.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onAddGroup() {
        this.ActiveProject.defaultGroups.push(this.NewGroup);
        this.NewGroup = Group.getDefault();
        this.IsDirty = true;
    }

    onAddCardToGroup(group: Group) {
        group.cards.push(this.NewCard);
        this.NewCard = Card.getDefault();
        this.IsDirty = true;
    }

    onDeleteCardFromGroup(group: Group, card: Card) {
        var index = 0;
        for (let c of group.cards) {
            if (c.id == card.id) {
                group.cards.splice(index, 1);
                break;
            }
            index++;
        }
        this.IsDirty = true;
    }

    onSave() {
        this.IsSaving = true;
        var response = this.projectService.save(this.ActiveProject);
        response.subscribe((data: boolean) => {
            this.IsSaving = false;
            this.IsSavingSuccessful = data;
            if (this.IsSavingSuccessful) {
                this.IsDirty = false;
            }
        });
    }

    onShareLink() {
        this.clipboardService.copyFromContent('http://localhost:4200/sort/' + this.ActiveProject.id);
        alert("Link to sort the cards has been copied to your clipboard.");
    }
}