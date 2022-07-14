using GeometryGym.Ifc;

var db = new DatabaseIfc(ReleaseVersion.IFC4X3_RC4);
var site = new IfcSite(db, "MySite");


var facility = new IfcFacility(db, "MyFacility");

var bridge = new IfcBridge(db)
{
    Name = "MyBridge",
    Description = "Just another bridge"
};

var project = new IfcProject(site, "MyProject", IfcUnitAssignment.Length.Metre);
{
};


var foundations = new IfcFacilityPart(
    facility,
    "Foundations",
    new IfcFacilityPartTypeSelect(IfcFacilityPartCommonTypeEnum.BELOWGROUND),
    IfcFacilityUsageEnum.NOTDEFINED
    );

var name = "Pile Cap";
var length = 1.2;
var width = 1.2;
var depth = 2.4;

IfcFootingType footingType = new IfcFootingType(db, name, IfcFootingTypeEnum.PAD_FOOTING);

footingType.RepresentationMaps.Add(
    new IfcRepresentationMap(
        db.Factory.XYPlanePlacement,
        new IfcShapeRepresentation(
            new IfcExtrudedAreaSolid(
                new IfcRectangleHollowProfileDef(db, name, length, width, depth),
                new IfcAxis2Placement3D(
                    new IfcCartesianPoint(db, 0 ,0, 0)
                    ),
                db.Factory.ZAxisNegative,
                depth
            )
        )
    )
);

db.WriteFile("IFC4X3RC4_testBridge.ifc");


